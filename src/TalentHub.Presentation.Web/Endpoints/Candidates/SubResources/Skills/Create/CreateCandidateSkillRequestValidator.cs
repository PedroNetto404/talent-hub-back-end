using FluentValidation;
using Humanizer;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.Skills.Create;

public sealed class CreateCandidateSkillRequestValidator :
    AbstractValidator<CreateCandidateSkillRequest>
{
    public CreateCandidateSkillRequestValidator()
    {
        RuleFor(s => s.CandidateId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(s => s.SkillId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(s => s.Proficiency)
            .NotNull()
            .NotEmpty()
            .Custom((p, ctx) =>
            {
                if (!Enum.TryParse<Proficiency>(p.Pascalize(), true, out _))
                {
                    ctx.AddFailure("Proficiency", $"Proficiency must be one of: {Enum.GetValues<Proficiency>().Select(p => p.ToString().Underscore())}");
                }
            });
    }
}
