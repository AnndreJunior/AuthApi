using AuthApi.Domain.Adapters.Services.Crypt;
using AuthApi.Infra.Services.Crypt;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AuthApi.Infra;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddInfra(this WebApplicationBuilder builder)
    {
        AddCryptService(builder);

        return builder;
    }

    private static void AddCryptService(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPasswordCrypt, PasswordCrypt>();
    }
}
