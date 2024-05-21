using AuthApi.Domain.Adapters.Repositories.Profile;
using AuthApi.Domain.Exceptions;

namespace AuthApi.Application.UseCases.Profile;
public class DeleteProfileUseCase
{
    private readonly IProfileRepository _profileRepository;

    public DeleteProfileUseCase(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task Execute(Guid id)
    {
        var userDoesNotExists = await _profileRepository.FindUserById(id) == null;
        if (userDoesNotExists)
            throw new NotFoundException("Usuário não encontrado");
        await _profileRepository.Delete(id);
    }
}
