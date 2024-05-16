using AuthApi.Communication.Requests;
using AuthApi.Communication.Responses;
using AuthApi.Domain.Adapters.Repositories.Profile;
using AuthApi.Domain.Exceptions;

namespace AuthApi.Application.UseCases.Profile;

public class ChangeUsernameUseCase
{
    private readonly IProfileRepository _profileRepository;

    public ChangeUsernameUseCase(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task<ProfileUpdateResponse> Execute(ChangeUsernameRequest request, Guid id)
    {
        var userDoesNotExists = await _profileRepository.FindUserById(id) == null;
        if (userDoesNotExists)
            throw new NotFoundException("Usuário não encontrado");

        var userUpdated = await _profileRepository.ChangeUsername(request.Username, id);

        return new ProfileUpdateResponse
        {
            Avatar = userUpdated.Avatar,
            Bio = userUpdated.Bio,
            Name = userUpdated.Name,
            Username = userUpdated.Username
        };
    }
}
