using FluentValidation;
using Humanizer;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.Experiences.Update;

public sealed class UpdateCandidateExperienceRequestValidator :
    AbstractValidator<UpdateCandidateExperienceRequest>
{
  public UpdateCandidateExperienceRequestValidator()
  {
    RuleFor(p => p.CandidateId)
        .NotEmpty()
        .NotNull()
        .NotEqual(Guid.Empty);

    RuleFor(p => p.ExperienceId)
        .NotEmpty()
        .NotNull()
        .NotEqual(Guid.Empty);

    RuleFor(p => p.Type)
        .NotEmpty()
        .NotNull()
        .Must(s => s == "academic" || s == "professional");

    RuleForEach(p => p.Activities)
        .NotEmpty()
        .NotNull()
        .MinimumLength(3)
        .MaximumLength(100);

    RuleForEach(p => p.AcademicEntities)
        .NotEmpty()
        .NotNull()
        .Must(a => Enum.TryParse<AcademicEntity>(a.Pascalize(), true, out _))
        .When(p => p.Type == "academic");

    RuleFor(p => p.CurrentSemester)
        .NotEmpty()
        .NotNull()
        .GreaterThan(0)
        .When(p => p.Type == "academic");

    RuleFor(p => p.Status)
        .NotEmpty()
        .NotNull()
        .Must(s => Enum.TryParse<ProgressStatus>(s.Pascalize(), true, out _))
        .When(p => p.Type == "academic");

    RuleFor(p => p.Description)
        .NotEmpty()
        .NotNull()
        .MinimumLength(3)
        .MaximumLength(500)
        .When(p => p.Type == "professional");
  }
}
