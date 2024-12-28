using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.Skills.Delete;

public sealed class DeleteCandidateSkillRequestValidator : AbstractValidator<DeleteCandidateSkillRequest>
{
    public DeleteCandidateSkillRequestValidator()
    {
        RuleFor(p => p.CandidateId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(p => p.CandidateSkillId)
            .NotEmpty()
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}
