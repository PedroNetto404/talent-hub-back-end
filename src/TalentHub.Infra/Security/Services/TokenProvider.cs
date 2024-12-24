using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Humanizer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Users;
using TalentHub.ApplicationCore.Resources.Users.ValueObjects;
using TalentHub.Infra.Security.Options;

namespace TalentHub.Infra.Security.Services;

public sealed class TokenProvider(
    IDateTimeProvider dateTimeProvider,
    IOptions<JwtOptions> jwtOptions 
) : ITokenProvider
{
    private readonly JwtOptions opt = jwtOptions.Value;

    public Token GenerateTokenFor(User user)
    {
        DateTime tokenExpiration = dateTimeProvider.UtcNow.AddMinutes(opt.AccessTokenExpiration);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString().Underscore())
            ]),
            Expires = tokenExpiration,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(opt.SecretKey)
                ),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Issuer = opt.Issuer,
            Audience = opt.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return new Token(
            tokenHandler.WriteToken(token),
            new DateTimeOffset(tokenExpiration).ToUnixTimeSeconds()
        );
    }

    public Token GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomNumber);
        return new Token(
            Convert.ToBase64String(randomNumber),
            new DateTimeOffset(
                dateTimeProvider
                    .UtcNow
                    .AddMinutes(opt.RefreshTokenExpiration)
            ).ToUnixTimeSeconds()
        );
    }
}
