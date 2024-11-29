using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Skills.Dtos;

namespace TalentHub.ApplicationCore.Skills.UseCases.Queries.GetSkillById;

public sealed record GetSkillByIdQuery(Guid SkillId) : ICachedQuery<SkillDto>
{
    public TimeSpan? Duration => TimeSpan.FromHours(12);

    public string Key => nameof(GetSkillByIdQuery);
}
