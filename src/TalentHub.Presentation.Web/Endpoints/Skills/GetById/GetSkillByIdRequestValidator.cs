using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Skills.GetById;

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