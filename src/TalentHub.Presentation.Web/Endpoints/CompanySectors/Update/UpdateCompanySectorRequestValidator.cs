using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.CompanySectors.Update;

public sealed class UpdateCompanySectorRequestValidator : AbstractValidator<UpdateCompanySectorRequest>
{
    public UpdateCompanySectorRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}
