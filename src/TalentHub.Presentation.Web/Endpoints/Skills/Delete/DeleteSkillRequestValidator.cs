using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Skills.Delete;

public sealed class DeleteSkillRequestValidator : AbstractValidator<DeleteSkillRequest>
{
    public DeleteSkillRequestValidator()
    {
        RuleFor(p => p.SkillId)
            .NotEmpty()
            .NotNull();
    }
}
