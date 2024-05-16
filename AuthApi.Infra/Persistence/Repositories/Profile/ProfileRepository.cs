using AuthApi.Domain.Adapters.Repositories.Profile;
using AuthApi.Domain.Entities;
using AuthApi.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Infra;

public class ProfileRepository : IProfileRepository
{
    private readonly AppDbContext _context;

    public ProfileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> FindUserById(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        return user;
    }

    public async Task<User> UploadAvatar(string avatarLink, Guid id)
    {
        var user = await _context.Users.FirstAsync(x => x.Id == id);

        user.AddAvatar(avatarLink);

        await _context.SaveChangesAsync();

        return user;
    }
}
