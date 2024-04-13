using AuthApi.Application.UseCases.Auth;
using AuthApi.Communication.Requests;
using AuthApi.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignUpUseCase _signUpUseCase;
    private readonly LoginUseCase _loginUseCase;
    private readonly GetUserDataUseCase _getUserDataUseCase;

    public AuthController(SignUpUseCase signUpUseCase, LoginUseCase loginUseCase, GetUserDataUseCase getUserDataUseCase)
    {
        _signUpUseCase = signUpUseCase;
        _loginUseCase = loginUseCase;
        _getUserDataUseCase = getUserDataUseCase;
    }

    [HttpPost("sign-up")]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> SignUp(SignUpRequest request)
    {
        var response = await _signUpUseCase.Execute(request);

        return Created(string.Empty, new AuthenticationResponse(response));
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await _loginUseCase.Execute(request);

        return Ok(new AuthenticationResponse(response));
    }

    /// <remarks>
    /// note: the avatar is a string because it's a public link to user's photo
    ///</remarks>
    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(typeof(AuthUserDataResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthUserDataResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUserData()
    {
        var tokenPayload = User.Identity?.Name;
        var userId = Guid.Parse(tokenPayload ?? "");

        var userData = await _getUserDataUseCase.Execute(userId);

        return Ok(userData);
    }
}
