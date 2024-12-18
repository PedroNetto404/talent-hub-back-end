using System.ComponentModel.DataAnnotations;

namespace TalentHub.Presentation.Web.Endpoints.Users.Authenticate;

public sealed record AuthenticateRequest
{
    [EmailAddress]
    public string? Email { get; init; }

    public string? Username { get; init; }

    [Required]
    public string Password { get; init; }
}
