using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Companies.RemovePresentationVideo;

public sealed class RemoveCompanyPresentationVideoRequestValidator :
    AbstractValidator<RemoveCompanyPresentationVideoRequest>
{
  public RemoveCompanyPresentationVideoRequestValidator()
  {
    RuleFor(p => p.CompanyId)
        .NotEmpty()
        .NotNull()
        .NotEqual(Guid.Empty);
  }
}
