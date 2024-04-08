namespace AuthApi.Domain.Adapters.Services.Crypt;

public interface IPasswordCrypt
{
    string HashPassword(string password);
    bool ComparePasswords(string password, string hashPassword);
}
