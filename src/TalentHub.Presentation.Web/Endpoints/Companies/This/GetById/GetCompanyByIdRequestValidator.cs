using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Companies.This.GetById;

public sealed class GetCompanyByIdRequestValidator : AbstractValidator<GetCompanyByIdRequest>
{
  public GetCompanyByIdRequestValidator()
  {
    RuleFor(p => p.CompanyId)
        .NotEmpty()
        .NotEqual(Guid.Empty);
  }
}
