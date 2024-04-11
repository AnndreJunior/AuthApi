using AuthApi.Domain.Adapters.Repositories.Auth;
using AuthApi.Domain.Adapters.Services.Crypt;
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

        return builder;
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
    }
}
