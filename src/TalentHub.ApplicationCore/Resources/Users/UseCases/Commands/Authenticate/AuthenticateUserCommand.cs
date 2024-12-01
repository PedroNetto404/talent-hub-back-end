using System.Text.Json.Serialization;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Users.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Authenticate;

public sealed record AuthenticateUserCommand(
    string? Email,
    string? Username,
    string Password
) : ICommand<AuthenticationResult>;

public sealed record AuthenticationResult
{
    public AuthenticationResult(
        Token accessToken,
        Token refreshToken
    )
    {
        (AccessToken, AccessTokenExpiration) = accessToken;
        (RefreshToken, RefreshTokenExpiration) = refreshToken;
    }

    [JsonPropertyName("access_token")]
    public string AccessToken { get; }

    [JsonPropertyName("access_token_expiration")]
    public long AccessTokenExpiration { get; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; }

    [JsonPropertyName("refresh_token_expiration")]
    public long RefreshTokenExpiration { get; }

    [JsonPropertyName("token_type")]
    public string TokenType => "Bearer";
}

