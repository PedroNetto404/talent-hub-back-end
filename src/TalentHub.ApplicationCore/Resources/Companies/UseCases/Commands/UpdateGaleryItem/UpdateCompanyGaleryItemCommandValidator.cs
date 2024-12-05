using FluentValidation;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.UpdateGaleryItem;

public sealed class UpdateCompanyGaleryCommandValidator : AbstractValidator<UpdateCompanyGaleryCommand>
{
    public UpdateCompanyGaleryCommandValidator()
    {
        RuleFor(x => x.CompanyId).NotEmpty();
        RuleFor(x => x.File).NotEmpty();
        RuleFor(x => x.FileContentType).NotEmpty();
    }
}
