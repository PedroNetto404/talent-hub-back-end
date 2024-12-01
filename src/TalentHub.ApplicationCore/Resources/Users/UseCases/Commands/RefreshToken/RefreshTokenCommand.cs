using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Authenticate;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.RefreshToken;

public sealed record RefreshTokenCommand(
    string RefreshToken
) : ICommand<AuthenticationResult>;
