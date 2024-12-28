using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Users.SubResouces.RefreshToken;

public sealed class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(p => p.RefreshToken).NotEmpty().NotNull();
    }
}
