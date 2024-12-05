using FluentValidation;
using TalentHub.ApplicationCore.Extensions;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.RemoveGaleryItem;

public sealed class RemoveCompanyGaleryItemCommandValidator : AbstractValidator<RemoveCompanyGaleryItemCommand>
{
    public RemoveCompanyGaleryItemCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .NotEqual(Guid.Empty)
            .NotEmpty()
            .WithMessage("companyId is required");

        RuleFor(x => x.Url)
            .Custom((url, context) =>
            {
                if (!url.IsValidUrl())
                {
                    context.AddFailure($"Url '{url}' is not a valid url");
                }
            })
            .NotEmpty()
            .WithMessage("wrl is required");
    }
}
