using AuthApi.Application.UseCases.Profile;
using AuthApi.Communication.Requests;
using AuthApi.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UploadAvatarUseCase _uploadAvatarUseCase;
    private readonly ChangeUsernameUseCase _changeUsernameUseCase;
    private readonly ChangePasswordUseCase _changePasswordUseCase;

    public UserController(
        UploadAvatarUseCase uploadAvatarUseCase,
        ChangeUsernameUseCase changeUsernameUseCase,
        ChangePasswordUseCase changePasswordUseCase)
    {
        _uploadAvatarUseCase = uploadAvatarUseCase;
        _changeUsernameUseCase = changeUsernameUseCase;
        _changePasswordUseCase = changePasswordUseCase;
    }

    [HttpPut("upload-avatar")]
    [ProducesResponseType(typeof(ProfileUpdateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UploadAvatar(IFormFile avatar)
    {
        var tokenPayload = User.Identity?.Name;
        var userId = Guid.Parse(tokenPayload ?? "");
        var result = await _uploadAvatarUseCase.Execute(avatar, userId);

        return Created(string.Empty, result);
    }

    [HttpPut("update-username")]
    [ProducesResponseType(typeof(ProfileUpdateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUsername(ChangeUsernameRequest request)
    {
        var tokenPayload = User.Identity?.Name;
        var userId = Guid.Parse(tokenPayload ?? "");
        var response = await _changeUsernameUseCase.Execute(request, userId);

        return Created(string.Empty, response);
    }

    [HttpPut("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        var tokenPayload = User.Identity?.Name;
        var userId = Guid.Parse(tokenPayload ?? "");
        await _changePasswordUseCase.Execute(request, userId);

        return Created();
    }
}
