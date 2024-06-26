﻿using AuthApi.Domain.Adapters.Repositories.Profile;
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

    public async Task<User> ChangeName(string name, Guid id)
    {
        var entity = await _context.Users.FirstAsync(x => x.Id ==  id);
        entity.UpdateName(name);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task ChangePassword(User user)
    {
        var entity = await _context.Users.FirstAsync(x => x.Id == user.Id);
        entity.UpdatePassword(user.Password);
        await _context.SaveChangesAsync();
    }

    public async Task<User> ChangeUsername(string username, Guid id)
    {
        var user = await _context.Users.FirstAsync(x => x.Id == id);

        user.UpdateUsername(username);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task Delete(Guid id)
    {
        var user = await _context.Users.FirstAsync(x => x.Id == id);
        user.DeleteUser();
        await _context.SaveChangesAsync();
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
