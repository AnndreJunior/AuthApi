using AuthApi.Domain.Adapters.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AuthApi.Validators;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IFileValidator, FileValidator>();

        return builder;
    }
}
