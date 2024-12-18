using Ardalis.Specification;
using Humanizer;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Users.Dtos;
using TalentHub.ApplicationCore.Resources.Users.Enums;
using TalentHub.ApplicationCore.Resources.Users.Specs;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Create;

public sealed class CreateUserCommandHandler(
    IRepository<User> userRepository,
    IPasswordHasher passwordHasher
) : ICommandHandler<CreateUserCommand, UserDto>
{
    public async Task<Result<UserDto>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        User? existing = await userRepository.FirstOrDefaultAsync(
            new GetUserByEmailOrUsernameSpec(request.Email, request.Username),
            cancellationToken
        );
        if (existing is not null)
        {
            return Error.BadRequest("invalid user credentials");
        }

        if (!Enum.TryParse(request.Role.Pascalize(), true, out Role role))
        {
            return new Error("user", "invalid role");
        }

        Result<User> userResult = User.Create(
            request.Email,
            request.Username,
            role,
            request.Password,
            passwordHasher
        );
        if (userResult.IsFail)
        {
            return userResult.Error;
        }

        await userRepository.AddAsync(userResult.Value, cancellationToken);
        return UserDto.FromEntity(userResult.Value);
    }
}
