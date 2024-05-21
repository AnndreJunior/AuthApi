using AuthApi.Communication.Requests;
using AuthApi.Communication.Responses;
using AuthApi.Domain.Adapters.Repositories.Profile;
using AuthApi.Domain.Exceptions;

namespace AuthApi.Application.UseCases.Profile;
public class ChangeNameUseCase
{
    private readonly IProfileRepository _profileRepository;

    public ChangeNameUseCase(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task<ProfileUpdateResponse> Execute(ChangeNameRequest request, Guid id)
    {
        var user = await _profileRepository.FindUserById(id)
            ?? throw new NotFoundException("Usuário não encontrado");
        var userUpdated = await _profileRepository.ChangeName(request.Name, id);

        return new ProfileUpdateResponse
        {
            Avatar = userUpdated.Avatar,
            Bio = userUpdated.Bio,
            Name = userUpdated.Name,
            Username = userUpdated.Username
        };
    }
}
