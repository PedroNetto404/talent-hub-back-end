using System.ComponentModel.DataAnnotations;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record AuthenticateRequest
{
    [EmailAddress]
    public string? Email { get; init; }

    public string? Username { get; init; }

    public string? Password { get; init; }

    [Required]
    [AllowedValues("refresh_token", "client_credentials")]
    public required string GrantType { get; init; }

    public string? RefreshToken { get; init; }
}
