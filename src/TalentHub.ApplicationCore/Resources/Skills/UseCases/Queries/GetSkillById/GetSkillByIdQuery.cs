using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Skills.Dtos;

namespace TalentHub.ApplicationCore.Resources.Skills.UseCases.Queries.GetSkillById;

public sealed record GetSkillByIdQuery(Guid SkillId) : CachedQuery<SkillDto>
{
    public override TimeSpan Duration => TimeSpan.FromHours(12);
}
