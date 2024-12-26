using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Companies.Delete;

public sealed class DeleteCompanyRequestValidator :
    AbstractValidator<DeleteCompanyRequest>
{
  public DeleteCompanyRequestValidator()
  {
    RuleFor(p => p.CompanyId)
        .NotEmpty()
        .NotEqual(Guid.Empty);
  }
}