using AuthApi.Domain.Entities;

namespace AuthApi.Domain.Adapters.Repositories.Profile;

public interface IProfileRepository
{
    Task<User> UploadAvatar(string avatarLink, Guid id);
    Task<bool> FindUserById(Guid id);
}
