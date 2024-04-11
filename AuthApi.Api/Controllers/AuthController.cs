using AuthApi.Application.UseCases.Auth;
using AuthApi.Communication.Requests;
using AuthApi.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignUpUseCase _signUpUseCase;
    private readonly LoginUseCase _loginUseCase;

    public AuthController(SignUpUseCase signUpUseCase, LoginUseCase loginUseCase)
    {
        _signUpUseCase = signUpUseCase;
        _loginUseCase = loginUseCase;
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
}
