using AuthApi.Domain.Adapters.Services.Crypt;
using AuthApi.Domain.Exceptions;

namespace AuthApi.Domain.Entities;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Name { get; private set; }
    public string? Bio { get; private set; }
    public string? Avatar { get; private set; }
    public bool IsDelete { get; private set; }

    public User(string username, string password, string name)
    {
        ValidateUserProps(username, password, name);

        Username = username.Trim();
        Password = password.Trim();
        Name = name.Trim();
    }

    private void ValidateUserProps(string username, string password, string name)
    {
        ValidateUsername(username);
        ValidatePassword(password);
        ValidateName(name);
    }

    public void AddAvatar(string avatar)
    {
        Avatar = avatar;
    }

    public void HashPassword(IPasswordCrypt passwordCrypt)
    {
        var hash = passwordCrypt.HashPassword(Password);
        Password = hash;
    }

    public void UpdateUsername(string username)
    {
        ValidateUsername(username);
        Username = username.Trim();
    }

    public void UpdatePassword(string password)
    {
        ValidatePassword(password);
        Password = password.Trim();
    }

    public void UpdateName(string name)
    {
        ValidateName(name);
        Name = name.Trim();
    }

    public void DeleteUser()
    {
        IsDelete = true;
    }

    private void ValidateUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ErrorOnValidationException("Informe seu nome de usuário");

        var usernameTooLong = username.Length > 100;
        if (usernameTooLong)
            throw new ErrorOnValidationException("Seu nome de usuário deve conter até 100 caracteres");
    }

    private void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ErrorOnValidationException("Informe sua senha");

        var passwordTooShort = password.Length < 6;
        if (passwordTooShort)
            throw new ErrorOnValidationException("Sua senha deve conter, pelo menos, seis caracteres");

        var passwordTooLong = password.Length > 200;
        if (passwordTooLong)
            throw new ErrorOnValidationException("Sua senha deve conter até 200 caracteres");
    }

    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ErrorOnValidationException("Informe seu nome");

        var nameTooLong = name.Length > 100;
        if (nameTooLong)
            throw new ErrorOnValidationException("Seu nome deve conter até 100 caracteres");
    }
}
