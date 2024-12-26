using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Universities.Delete;

public sealed class DeleteUniversityRequestValidator : AbstractValidator<DeleteUniversityRequest>
{
  public DeleteUniversityRequestValidator()
  {
    RuleFor(x => x.UniversityId)
        .NotNull()
        .NotEqual(Guid.Empty)
        .NotEmpty();
  }
}
