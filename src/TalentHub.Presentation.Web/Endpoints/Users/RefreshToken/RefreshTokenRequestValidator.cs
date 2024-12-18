using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Users.RefreshToken;

public sealed class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(p => p.RefreshToken).NotEmpty().NotNull();
    }
}
