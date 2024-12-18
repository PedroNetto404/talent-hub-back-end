using System.ComponentModel.DataAnnotations;

namespace TalentHub.Presentation.Web.Endpoints.Users.Create;

public sealed record CreateUserRequest
{
    [Required]
    [EmailAddress]
    public required string Email { get; init; }

    [Required]
    public required string Username { get; init; }

    [Required]
    public required string Password { get; init; }

    [Required]
    [AllowedValues(
        "admin",
        "candidate",
        "company"
    )]
    public required string Role { get; init; }
}

