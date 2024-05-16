using AuthApi.Application.UseCases.Profile;
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

    public UserController(UploadAvatarUseCase uploadAvatarUseCase)
    {
        _uploadAvatarUseCase = uploadAvatarUseCase;
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
}
