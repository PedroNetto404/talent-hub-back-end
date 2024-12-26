using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Skills.Dtos;
using TalentHub.ApplicationCore.Resources.Skills.Enums;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.ApplicationCore.Resources.Skills.UseCases.Queries.GetAllSkills;

public sealed record GetAllSkillsQuery(
    IEnumerable<Guid> Ids,
    SkillType? Type,
    int Limit,
    int Offset,
    string? SortBy = null,
    SortOrder SortOrder = SortOrder.Ascending
) : CachedQuery<PageResponse<SkillDto>>
{
    public override TimeSpan Duration => TimeSpan.FromHours(12);
}
