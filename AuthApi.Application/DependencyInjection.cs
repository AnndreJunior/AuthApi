using AuthApi.Application.UseCases.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AuthApi.Application;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<SignUpUseCase>();
        builder.Services.AddScoped<LoginUseCase>();
        builder.Services.AddScoped<GetUserDataUseCase>();

        return builder;
    }
}
