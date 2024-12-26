using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.GetById;

public sealed class GetCandidateByIdRequestValidator : AbstractValidator<GetCandidateByIdRequest>
{
  public GetCandidateByIdRequestValidator()
  {
    RuleFor(p => p.CandidateId)
        .NotEmpty()
        .NotEmpty()
        .NotEqual(Guid.Empty);
  }
}
