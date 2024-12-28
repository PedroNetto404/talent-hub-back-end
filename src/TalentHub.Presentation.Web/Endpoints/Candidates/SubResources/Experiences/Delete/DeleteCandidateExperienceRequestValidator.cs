using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.Experiences.Delete;

public sealed class DeleteCandidateExperienceRequestValidator : 
    AbstractValidator<DeleteCandidateExperienceRequest>
{
    public DeleteCandidateExperienceRequestValidator()
    {
        RuleFor(x => x.CandidateId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(x => x.ExperienceId)
            .NotEmpty()
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}
