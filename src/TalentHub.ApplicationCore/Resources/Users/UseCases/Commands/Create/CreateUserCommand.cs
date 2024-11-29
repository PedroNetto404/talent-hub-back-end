using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Users.Dtos;

namespace TalentHub.ApplicationCore.Users.UseCases.Commands.Create;

public sealed record CreateUserCommand(
    string Email,
    string Username,
    string Password,
    string Role
) : ICommand<UserDto>;
