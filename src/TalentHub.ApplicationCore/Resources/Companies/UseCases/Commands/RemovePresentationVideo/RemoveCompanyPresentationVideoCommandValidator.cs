using FluentValidation;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.RemovePresentationVideo;

public sealed class RemoveCompanyPresentationVideoCommandValidator : AbstractValidator<RemoveCompanyPresentationVideoCommand>
{
    public RemoveCompanyPresentationVideoCommandValidator()
    {
        RuleFor(x => x.CompanyId).NotEmpty().NotEqual(Guid.Empty);
    }
}
