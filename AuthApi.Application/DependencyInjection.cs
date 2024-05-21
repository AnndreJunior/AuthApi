using AuthApi.Application.UseCases.Auth;
using AuthApi.Application.UseCases.Profile;
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
        builder.Services.AddScoped<UploadAvatarUseCase>();
        builder.Services.AddScoped<ChangeUsernameUseCase>();
        builder.Services.AddScoped<ChangePasswordUseCase>();
        builder.Services.AddScoped<ChangeNameUseCase>();
        builder.Services.AddScoped<DeleteProfileUseCase>();

        return builder;
    }
}
