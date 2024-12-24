namespace TalentHub.Presentation.Web.Endpoints.Users.Authenticate;

public sealed record AuthenticateRequest(
    string? Email,
    string? Username,
    string Password
);
