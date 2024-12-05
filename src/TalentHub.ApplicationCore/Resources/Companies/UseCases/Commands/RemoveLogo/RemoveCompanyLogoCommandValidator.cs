using FluentValidation;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.RemoveLogo;

public sealed class RemoveCompanyLogoCommandValidator : AbstractValidator<RemoveCompanyLogoCommand>
{
    public RemoveCompanyLogoCommandValidator()
    {
        RuleFor(x => x.CompanyId).NotEmpty().NotEqual(Guid.Empty);
    }
}
