using FluentValidation;
using Humanizer;
using TalentHub.ApplicationCore.Resources.Skills.Enums;

namespace TalentHub.Presentation.Web.Endpoints.Skills.Update;

public sealed class UpdateSkillRequestValidator : AbstractValidator<UpdateSkillRequest>
{
    public UpdateSkillRequestValidator()
    {
        RuleFor(p => p.SkillId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(p => p.Type)
            .NotNull()
            .NotEmpty()
            .Custom((s, ctx) =>
            {
                if (!Enum.TryParse<SkillType>(s.Pascalize(), true, out _))
                {
                    ctx.AddFailure("Skill Type", $"Skill type must be one of: {Enum.GetNames<SkillType>().Select(p => p.ToString().Underscore())}");
                }
            });

        RuleForEach(p => p.Tags)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(200)
            .When(p => p.Tags?.Any() ?? false);

    }
}
