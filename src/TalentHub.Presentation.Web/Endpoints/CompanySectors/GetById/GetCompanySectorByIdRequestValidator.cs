using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.CompanySectors.GetById;

public sealed class GetCompanySectorByIdRequestValidator :
    AbstractValidator<GetCompanySectorByIdRequest>
{
  public GetCompanySectorByIdRequestValidator()
  {
    RuleFor(x => x.Id).NotEmpty().NotNull().NotEqual(Guid.Empty);
  }
}
