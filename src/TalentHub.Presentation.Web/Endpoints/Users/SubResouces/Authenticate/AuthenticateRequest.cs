namespace TalentHub.Presentation.Web.Endpoints.Users.SubResouces.Authenticate;

public sealed record AuthenticateRequest(
    string? Email,
    string? Username,
    string Password
);
