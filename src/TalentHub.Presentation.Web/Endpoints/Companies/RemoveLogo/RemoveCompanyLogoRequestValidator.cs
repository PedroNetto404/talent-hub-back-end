using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Companies.RemoveLogo;

public sealed class RemoveCompanyLogoRequestValidator :
    AbstractValidator<RemoveCompanyLogoRequest>
{
    public RemoveCompanyLogoRequestValidator()
    {
        RuleFor(p => p.CompanyId)
            .NotEmpty()
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}
