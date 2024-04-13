using AuthApi.Communication.Responses;
using AuthApi.Domain.Adapters.Repositories.Auth;
using AuthApi.Domain.Exceptions;

namespace AuthApi.Application.UseCases.Auth;

public class GetUserDataUseCase
{
    private readonly IAuthRepository _authRepository;

    public GetUserDataUseCase(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<AuthUserDataResponse?> Execute(Guid userId)
    {
        var user = await _authRepository.GetUserData(userId)
            ?? throw new NotFoundException("Usuário não encontrado");

        return new AuthUserDataResponse
        {
            Username = user.Username,
            Name = user.Name,
            Avatar = user.Avatar,
            Bio = user.Bio
        };
    }
}
