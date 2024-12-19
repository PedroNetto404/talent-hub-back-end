using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.GetAll;

public sealed class GetCandidatesRequestValidator : AbstractValidator<GetCandidatesRequest>
{
    public GetCandidatesRequestValidator()
    {
        When(
            p => p.Languages!.Any(), 
            () => RuleFor(p => p.Languages).ForEach(b => b.NotEmpty().NotNull()));
        
        When(
            p => p.SkillIds!.Any(),
            () => RuleFor(q => q.SkillIds)
                .ForEach(k => k.NotEmpty().NotNull().NotEqual(Guid.Empty))
        );
    }
}
