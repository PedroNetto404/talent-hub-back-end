using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Humanizer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Users;
using TalentHub.ApplicationCore.Resources.Users.ValueObjects;

namespace TalentHub.Infra.Security.Services;

public sealed class TokenProvider(
    IConfiguration configuration,
    IDateTimeProvider dateTimeProvider
) : ITokenProvider
{
    public Token GenerateTokenFor(User user)
    {
        int tokenExpirationInMinutes =
            int.Parse(configuration["Jwt:AccessTokenExpirationInMinutes"]!);

        System.DateTime tokenExpiration = dateTimeProvider.UtcNow.AddMinutes(tokenExpirationInMinutes);

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
                    Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]!)
                ),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
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
                    .AddMinutes(
                        int.Parse(
                            configuration["Jwt:RefreshTokenExpirationInMinutes"]!
                        )
                    )
            ).ToUnixTimeSeconds()
        );
    }
}
