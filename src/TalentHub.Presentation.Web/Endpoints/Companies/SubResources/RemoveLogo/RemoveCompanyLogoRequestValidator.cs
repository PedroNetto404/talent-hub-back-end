using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Companies.SubResources.RemoveLogo;

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
