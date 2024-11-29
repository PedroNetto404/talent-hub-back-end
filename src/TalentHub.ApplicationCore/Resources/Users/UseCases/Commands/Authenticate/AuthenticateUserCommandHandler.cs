using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Users.Specs;
using TalentHub.ApplicationCore.Users.ValueObjects;

namespace TalentHub.ApplicationCore.Users.UseCases.Commands.Authenticate;

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
        User? user;
        if(request.Email is not null) 
        {
            user = await userRepository.FirstOrDefaultAsync(
                new GetUserByEmailSpec(
                    request.Email
                ),
                cancellationToken
            );
        } else if(request.Username is not null)
        {
            user = await userRepository.FirstOrDefaultAsync(
                new GetUserByUsernameSpec(
                    request.Username
                ),
                cancellationToken
            );
        } else 
        {
            return new Error("user", "invalid user name or email");
        }

        if(user is null)
        {
            return NotFoundError.Value;
        }

        if(!passwordHasher.Match(request.Password, user.HashedPassword))
        {
            return new Error("user", "invalid user credentiais");
        }

        if(user.CanRefreshToken(dateTimeProvider)) 
        {
            return new Error("user", "user can refresh token");
        }

        Token refreshToken = tokenProvider.GenerateRefreshToken(user);
        user.SetRefreshToken(refreshToken, dateTimeProvider);

        await userRepository.UpdateAsync(user, cancellationToken);

        Token accessToken = tokenProvider.GenerateTokenFor(user);

        return new AuthenticationResult
        {
            AccessToken = accessToken.Value,
            AccessTokenExpiration = accessToken.Expiration,
            RefreshToken = refreshToken.Value,
            RefreshTokenExpiration = refreshToken.Expiration
        };
    }
}
    
