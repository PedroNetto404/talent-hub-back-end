using System.Text.RegularExpressions;
using FluentValidation;
using TalentHub.ApplicationCore.Resources.Users;
using TalentHub.ApplicationCore.Resources.Users.Enums;

namespace TalentHub.Presentation.Web.Endpoints.Users.This.Create;

public sealed class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();

        RuleFor(p => p.Username)
            .MinimumLength(5)
            .MaximumLength(20)
            .NotEmpty()
            .NotNull();

        RuleFor(p => p.Password)
            .MinimumLength(8)
            .MaximumLength(20)
            .Matches(new Regex($@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{{{User.PasswordMinLength},{User.PasswordMaxLength}}}$"))
            .WithMessage($"Password must be between {User.PasswordMinLength} and {User.PasswordMaxLength} characters long and contain at least one uppercase letter, one lowercase letter, and one number.")
            .NotEmpty()
            .NotNull();

        RuleFor(p => p.Role)
            .NotNull()
            .NotEmpty()
            .Custom((role, context) =>
            {
                if(!Role.TryFromName(role, true, out Role _))
                {
                    context.AddFailure($"Role must be one of: {string.Join(", ", Role.List.Select(p => p.Name))}");
                }
            });
    }
}
