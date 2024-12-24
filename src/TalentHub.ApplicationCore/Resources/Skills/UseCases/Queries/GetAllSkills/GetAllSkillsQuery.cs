using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.ApplicationCore.Resources.Skills.UseCases.Queries.GetAllSkills;

public sealed record GetAllSkillsQuery(
    IEnumerable<Guid> Ids,
    int Limit,
    int Offset,
    string? SortBy = null,
    SortOrder SortOrder = SortOrder.Ascending
) : CachedQuery<PageResponse>
{
    public override TimeSpan Duration => TimeSpan.FromHours(12);
}
