using AuthApi.Domain.Adapters.Services.Crypt;

namespace AuthApi.Infra.Services.Crypt;

public class PasswordCrypt : IPasswordCrypt
{
    private const int _workFactor = 12;

    public bool ComparePasswords(string password, string hashPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashPassword);
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, _workFactor);
    }
}
