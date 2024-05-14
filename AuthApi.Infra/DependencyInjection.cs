using AuthApi.Domain.Adapters.Repositories.Auth;
using AuthApi.Domain.Adapters.Repositories.Profile;
using AuthApi.Domain.Adapters.Services.Crypt;
using AuthApi.Domain.Adapters.Services.File;
using AuthApi.Domain.Adapters.Services.Jwt;
using AuthApi.Infra.Persistence;
using AuthApi.Infra.Persistence.Repositories.Auth;
using AuthApi.Infra.Services.Crypt;
using AuthApi.Infra.Services.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthApi.Infra;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddInfra(this WebApplicationBuilder builder)
    {
        AddCryptService(builder);
        AddJwtService(builder);
        AddDbContext(builder);
        AddRepositories(builder);
        AddFileService(builder);

        return builder;
    }

    public static void AddFileService(WebApplicationBuilder builder)
    {
        var supabaseUrl = builder.Configuration["supabase:url"]
            ?? throw new Exception("Supabase url not found");
        var supabaseKey = builder.Configuration["supabase:key"]
            ?? throw new Exception("Supabase key not found");

        builder.Services.AddScoped<IFileService>(profiver => new FileService(supabaseUrl, supabaseKey));
    }

    private static void AddCryptService(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPasswordCrypt, PasswordCrypt>();
    }

    private static void AddJwtService(WebApplicationBuilder builder)
    {
        var jwtSecret = builder.Configuration["jwt:secret"] ?? throw new Exception("Jwt secret not found");
        builder.Services.AddTransient<IJwtService>(provider => new JwtService(jwtSecret));
    }

    private static void AddDbContext(WebApplicationBuilder builder)
    {
        var dbConnectionString = builder.Configuration["database:connection"]
            ?? throw new Exception("Database connection string not found");
        builder.Services.AddDbContext<AppDbContext>(opts
        => opts.UseNpgsql(dbConnectionString));
    }

    private static void AddRepositories(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();
        builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
    }
}
