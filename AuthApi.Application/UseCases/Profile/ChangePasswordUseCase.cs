using AuthApi.Communication.Requests;
using AuthApi.Domain.Adapters.Repositories.Profile;
using AuthApi.Domain.Adapters.Services.Crypt;
using AuthApi.Domain.Exceptions;

namespace AuthApi.Application.UseCases.Profile;
public class ChangePasswordUseCase
{
    private readonly IProfileRepository _profileRepository;
    private readonly IPasswordCrypt _passwordCrypt;

    public ChangePasswordUseCase(IProfileRepository profileRepository, IPasswordCrypt passwordCrypt)
    {
        _profileRepository = profileRepository;
        _passwordCrypt = passwordCrypt;
    }

    public async Task Execute(ChangePasswordRequest request, Guid id)
    {
        var user = await _profileRepository.FindUserById(id)
            ?? throw new NotFoundException("Usuário não encontrado");
        user.UpdatePassword(request.NewPassword);
        user.HashPassword(_passwordCrypt);
        await _profileRepository.ChangePassword(user);
    }
}
