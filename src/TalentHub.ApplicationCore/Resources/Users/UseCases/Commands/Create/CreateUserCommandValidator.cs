using FluentValidation;
using TalentHub.ApplicationCore.Resources.Users.Enums;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Create;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Role).NotEmpty().IsEnumName(typeof(Role), false);
    }
}
