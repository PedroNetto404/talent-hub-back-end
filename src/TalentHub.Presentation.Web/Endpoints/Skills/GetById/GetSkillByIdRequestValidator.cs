using FluentValidation;
using TalentHub.ApplicationCore.Resources.Skills;

public sealed class GetSkillByIdRequestValidator :
    AbstractValidator<GetSkillByIdRequest>
{
    public GetSkillByIdRequestValidator()
    {
        RuleFor(p => p.SkillId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}
