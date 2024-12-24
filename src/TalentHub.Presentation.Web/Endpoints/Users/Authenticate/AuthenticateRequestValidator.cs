using System.Text.RegularExpressions;
using FluentValidation;
using TalentHub.ApplicationCore.Resources.Users;

namespace TalentHub.Presentation.Web.Endpoints.Users.Authenticate;

public sealed class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
{
    public AuthenticateRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Username)
            .NotEmpty()
            .When(x => string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(User.PasswordMinLength)
            .MaximumLength(User.PasswordMaxLength)
            .Matches(new Regex($@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{{{User.PasswordMinLength},{User.PasswordMaxLength}}}$"))
            .WithMessage($"Password must be between {User.PasswordMinLength} and {User.PasswordMaxLength} characters long and contain at least one uppercase letter, one lowercase letter, and one number.")
            .NotEmpty();
    }
}
