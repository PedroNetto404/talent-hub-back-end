using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Authenticate;
using TalentHub.ApplicationCore.Resources.Users.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler(
    IRepository<User> userRepository,
    IDateTimeProvider dateTimeProvider,
    ITokenProvider tokenProvider
) : ICommandHandler<RefreshTokenCommand, AuthenticationResult>
{
    public async Task<Result<AuthenticationResult>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken
    )
    {
        if (!Guid.TryParse(request.RefreshToken[..36], out Guid userId))
        {
            return new Error("user", "invalid refresh token");
        }

        User? user = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            return Error.NotFound("user");
        }

        if(
            !user.CanRefreshToken(dateTimeProvider) 
            || user.RefreshToken!.Value != request.RefreshToken
        )
        {
            return new Error("user", "invalid refresh token");
        }

        Token accessToken = tokenProvider.GenerateTokenFor(user);
        return new AuthenticationResult(accessToken, user.RefreshToken);
    }
}
