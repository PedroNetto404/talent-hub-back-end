using FluentValidation;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Delete;

public sealed class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
{
    public DeleteCompanyCommandValidator()
    {
        RuleFor(p => p.CompanyId)
            .NotEqual(Guid.Empty);
    }
}
