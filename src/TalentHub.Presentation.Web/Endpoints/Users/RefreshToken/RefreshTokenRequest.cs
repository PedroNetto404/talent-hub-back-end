using System.ComponentModel.DataAnnotations;

namespace TalentHub.Presentation.Web.Endpoints.Users.RefreshToken;

public sealed record RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; init; }
}
