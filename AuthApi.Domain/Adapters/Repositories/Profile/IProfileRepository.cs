using AuthApi.Domain.Entities;

namespace AuthApi.Domain.Adapters.Repositories.Profile;

public interface IProfileRepository
{
    Task<User> UploadAvatar(string avatarLink, Guid id);
    Task<User?> FindUserById(Guid id);
}
