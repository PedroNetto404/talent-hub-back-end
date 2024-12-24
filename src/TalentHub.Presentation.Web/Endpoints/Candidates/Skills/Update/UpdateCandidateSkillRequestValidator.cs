using FluentValidation;
using Humanizer;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Skills.Update;

public sealed class UpdateCandidateSkillRequestValidator : AbstractValidator<UpdateCandidateSkillRequest>
{
    public UpdateCandidateSkillRequestValidator()
    {
        RuleFor(p => p.CandidateId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(p => p.CandidateSkillId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(p => p.Proficiency)
            .NotNull()
            .NotEmpty()
            .Custom((p, ctx) =>
            {
                if (!Enum.TryParse<Proficiency>(p.Pascalize(), true, out _))
                {
                    ctx.AddFailure("Proficiency", $"Proficiency must be one of : {Enum.GetValues<Proficiency>().Select(p => p.ToString().Underscore())}");
                }
            });
    }
}
