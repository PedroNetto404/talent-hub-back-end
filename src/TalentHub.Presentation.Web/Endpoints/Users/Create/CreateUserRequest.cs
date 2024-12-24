namespace TalentHub.Presentation.Web.Endpoints.Users.Create;

public sealed record CreateUserRequest(
    string Email,
    string Username,
    string Password,
    string Role
);
