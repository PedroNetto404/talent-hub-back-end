using System.Text.Json.Serialization;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Users.UseCases.Commands.Authenticate;

public sealed record AuthenticateUserCommand(
    string? Email,
    string? Username,
    string Password
) : ICommand<AuthenticationResult>;

public sealed record AuthenticationResult
{
    [JsonPropertyName("access_token")]
    public required string AccessToken { get; init; }

    [JsonPropertyName("access_token_expiration")]
    public required long AccessTokenExpiration { get; init; }

    [JsonPropertyName("refresh_token")]
    public required string RefreshToken { get; init; }

    [JsonPropertyName("refresh_token_expiration")]
    public required long RefreshTokenExpiration { get; init; }

    [JsonPropertyName("token_type")]
    public string TokenType => "Bearer";
}
    
