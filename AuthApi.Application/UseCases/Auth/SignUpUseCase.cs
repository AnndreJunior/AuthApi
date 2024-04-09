using AuthApi.Communication.Requests;
using AuthApi.Domain.Adapters.Repositories.Auth;
using AuthApi.Domain.Adapters.Services.Crypt;
using AuthApi.Domain.Adapters.Services.Jwt;
using AuthApi.Domain.Entities;
using AuthApi.Domain.Exceptions;

namespace AuthApi.Application.UseCases.Auth;

public class SignUpUseCase
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordCrypt _passwordCryptService;

    public SignUpUseCase(IAuthRepository authRepository, IJwtService jwtService, IPasswordCrypt passwordCryptService)
    {
        _authRepository = authRepository;
        _jwtService = jwtService;
        _passwordCryptService = passwordCryptService;
    }

    public async Task<string> Execute(SignUpRequest request)
    {
        var (Username, Password, Name) = request;
        var entity = new User(_passwordCryptService, Username, Password, Name);

        var userAlreadyExists = await _authRepository.CheckIfUserExists(Username);
        if (userAlreadyExists)
            throw new ConflictException("Usuário já existe");

        await _authRepository.SignUp(entity);

        var token = _jwtService.GenerateToken(entity.Id);

        return token;
    }
}
