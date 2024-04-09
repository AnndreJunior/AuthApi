using AuthApi.Domain.Entities;

namespace AuthApi.Domain.Adapters.Repositories.Auth;

public interface IAuthRepository
{
    Task SignUp(User user);
    Task<bool> CheckIfUserExists(string username);
}
