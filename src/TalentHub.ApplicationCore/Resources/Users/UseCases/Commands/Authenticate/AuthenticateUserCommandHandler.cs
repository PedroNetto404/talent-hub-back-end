using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Users.Specs;
using TalentHub.ApplicationCore.Resources.Users.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Authenticate;

public sealed class AuthenticateUserCommandHandler(
    ITokenProvider tokenProvider,
    IRepository<User> userRepository,
    IDateTimeProvider dateTimeProvider,
    IPasswordHasher passwordHasher
) :
    ICommandHandler<AuthenticateUserCommand, AuthenticationResult>
{
    public async Task<Result<AuthenticationResult>> Handle(
        AuthenticateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        User? user = await userRepository.FirstOrDefaultAsync(
            new GetUserByEmailOrUsernameSpec(request.Email, request.Username),
            cancellationToken
        );
        if(user is null)
        {
            return Error.NotFound("user", expose: false);
        }

        if (!passwordHasher.Match(request.Password, user.HashedPassword))
        {
            return new Error("user", "invalid user credentiais");
        }

        if (user.CanRefreshToken(dateTimeProvider))
        {
            return Error.BadRequest("user can refresh token");
        }

        Token refreshToken = tokenProvider.GenerateRefreshToken();
        user.SetRefreshToken(refreshToken, dateTimeProvider);

        await userRepository.UpdateAsync(user, cancellationToken);

        Token accessToken = tokenProvider.GenerateTokenFor(user);

        return new AuthenticationResult(accessToken, refreshToken);
    }
}

