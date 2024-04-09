using AuthApi.Domain.Adapters.Services.Crypt;
using AuthApi.Domain.Adapters.Services.Jwt;
using AuthApi.Infra.Services.Crypt;
using AuthApi.Infra.Services.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AuthApi.Infra;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddInfra(this WebApplicationBuilder builder)
    {
        AddCryptService(builder);
        AddJwtService(builder);

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
}
