using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Universities.GetById;

public sealed class GetUniversityByIdRequestValidator :
    AbstractValidator<GetUniversityByIdRequest>
{
  public GetUniversityByIdRequestValidator()
  {
    RuleFor(p => p.UniversityId)
        .NotNull()
        .NotEqual(Guid.Empty)
        .NotEmpty();
  }
}
