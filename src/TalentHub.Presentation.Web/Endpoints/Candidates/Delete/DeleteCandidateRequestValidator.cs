using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Delete;

public sealed class DeleteCandidateRequestValidator : AbstractValidator<DeleteCandidateRequest>
{
    public DeleteCandidateRequestValidator()
    {
        RuleFor(p => p.CandidateId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}
