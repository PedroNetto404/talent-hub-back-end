using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Users.UseCases.Commands.Authenticate;

namespace TalentHub.ApplicationCore.Users.UseCases.Commands.RefreshToken;

public sealed record RefreshTokenCommand(
    string RefreshToken
) : ICommand<AuthenticationResult>;
