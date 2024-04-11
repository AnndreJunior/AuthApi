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

    public AuthController(SignUpUseCase signUpUseCase)
    {
        _signUpUseCase = signUpUseCase;
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
}
