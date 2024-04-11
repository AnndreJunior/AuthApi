using AuthApi.Communication.Requests;
using AuthApi.Domain.Adapters.Repositories.Auth;
using AuthApi.Domain.Adapters.Services.Crypt;
using AuthApi.Domain.Adapters.Services.Jwt;
using AuthApi.Domain.Exceptions;

namespace AuthApi.Application.UseCases.Auth;

public class LoginUseCase
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordCrypt _passwordCryptService;

    public LoginUseCase(IAuthRepository authRepository, IJwtService jwtService, IPasswordCrypt passwordCryptService)
    {
        _authRepository = authRepository;
        _jwtService = jwtService;
        _passwordCryptService = passwordCryptService;
    }

    public async Task<string> Execute(LoginRequest request)
    {
        var user = await _authRepository.Login(request.Username)
            ?? throw new NotFoundException("Usuário não encontrado");

        var passwordIsIncorrect = _passwordCryptService.ComparePasswords(request.Password, user.Password) == false;
        if (passwordIsIncorrect)
            throw new ErrorOnValidationException("Senha incorreta");

        var token = _jwtService.GenerateToken(user.Id);

        return token;
    }
}
