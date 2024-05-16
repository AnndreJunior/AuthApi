using AuthApi.Communication.Responses;
using AuthApi.Domain.Adapters.Repositories.Profile;
using AuthApi.Domain.Adapters.Services.File;
using AuthApi.Domain.Adapters.Validators;
using AuthApi.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace AuthApi.Application.UseCases.Profile;

public class UploadAvatarUseCase
{
    private readonly IProfileRepository _profileRepository;
    private readonly IFileService _fileService;
    private readonly IFileValidator _fileValidator;

    public UploadAvatarUseCase(IProfileRepository profileRepository, IFileService fileService, IFileValidator fileValidator)
    {
        _profileRepository = profileRepository;
        _fileService = fileService;
        _fileValidator = fileValidator;
    }

    public async Task<ProfileUpdateResponse> Execute(IFormFile avatar, Guid id)
    {
        var userDoesNotExists = await _profileRepository.FindUserById(id) == null;
        if (userDoesNotExists)
            throw new ConflictException("Usuário não encontrado");

        var isNotImage = _fileValidator.IsImage(avatar.OpenReadStream()) == false;
        if (isNotImage)
            throw new ErrorOnValidationException("Envie uma imagem válida");

        var memoryStream = new MemoryStream();
        await avatar.CopyToAsync(memoryStream);

        var avatarContent = memoryStream.ToArray();

        var originalFileName = Path.GetFileName(avatar.FileName);
        var fileExtention = Path.GetExtension(originalFileName);
        var uploadFileName = $"{Guid.NewGuid()}{fileExtention}";

        var avatarLink = await _fileService.UploadAvatar(avatarContent, uploadFileName);

        var user = await _profileRepository.UploadAvatar(avatarLink, id);

        return new ProfileUpdateResponse
        {
            Name = user.Name,
            Avatar = user.Avatar,
            Bio = user.Bio,
            Username = user.Username
        };
    }
}
