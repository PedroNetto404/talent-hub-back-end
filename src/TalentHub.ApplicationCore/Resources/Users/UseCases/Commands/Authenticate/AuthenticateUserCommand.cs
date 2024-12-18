using FluentValidation;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Authenticate;

public sealed record AuthenticateUserCommand(
    string? Email,
    string? Username,
    string Password
) : ICommand<AuthenticationResult>;

public sealed class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserCommandValidator()
    {
        When(p => p.Email is null, () => RuleFor(p => p.Username).NotEmpty().NotNull());
        When(p => p.Username is null, () => RuleFor(q => q.Email).NotNull().NotEmpty().EmailAddress());
        RuleFor(p => p.Password).NotEmpty().NotNull();
    }
}
