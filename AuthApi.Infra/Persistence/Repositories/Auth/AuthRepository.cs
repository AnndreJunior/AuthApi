using AuthApi.Domain.Adapters.Repositories.Auth;
using AuthApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Infra.Persistence.Repositories.Auth;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckIfUserExists(string username)
    {
        var userExists = await _context.Users.FirstOrDefaultAsync(user => user.Username == username) is not null;

        return userExists;
    }

    public async Task<User?> Login(string username)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.Username == username && user.IsDelete == false);

        return user;
    }

    public async Task SignUp(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
