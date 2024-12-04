using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Authenticate;

public sealed record AuthenticateUserCommand(
    string? Email,
    string? Username,
    string Password
) : ICommand<AuthenticationResult>;