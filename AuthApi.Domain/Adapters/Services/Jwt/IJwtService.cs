namespace AuthApi.Domain.Adapters.Services.Jwt;

public interface IJwtService
{
    string GenerateToken(Guid id);
}
