using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.CompanySectors.Create;

public sealed class CreateCompanySectorRequestValidator : AbstractValidator<CreateCompanySectorRequest>
{
  public CreateCompanySectorRequestValidator()
  {
    RuleFor(x => x.Name)
        .NotNull()
        .NotEmpty()
        .MinimumLength(3)
        .MaximumLength(100);
  }
}
