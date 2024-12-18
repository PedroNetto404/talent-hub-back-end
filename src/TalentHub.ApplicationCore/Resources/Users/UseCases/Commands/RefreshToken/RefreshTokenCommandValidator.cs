using FluentValidation;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.RefreshToken;

public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator() 
    {
        RuleFor(p => p.UserId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(p => p.RefreshToken).NotNull().NotEmpty();
    }
}
