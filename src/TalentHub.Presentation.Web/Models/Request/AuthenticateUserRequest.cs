using System.ComponentModel.DataAnnotations;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record AuthenticateUserRequest
{
    [EmailAddress]
    public string? Email { get; init; }

    public string? Username { get; init; }

    [Required]
    public required string Password { get; init; }
}
