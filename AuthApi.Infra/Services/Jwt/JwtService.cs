using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthApi.Domain.Adapters.Services.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace AuthApi.Infra.Services.Jwt;

public class JwtService : IJwtService
{
    private readonly string _secret;

    public JwtService(string secret)
    {
        _secret = secret;
    }

    public string GenerateToken(Guid id)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature
        );
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(2),
            Subject = GenerateClaim(id)
        };

        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }

    private ClaimsIdentity GenerateClaim(Guid id)
    {
        var claimIdentity = new ClaimsIdentity();
        claimIdentity.AddClaim(new Claim(ClaimTypes.Name, id.ToString()));

        return claimIdentity;
    }
}
