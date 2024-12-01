using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Skills.Dtos;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Skills.UseCases.Queries.GetAllSkills;

public sealed record GetAllSkillsQuery(
    IEnumerable<Guid> Ids,
    int Limit,
    int Offset,
    string? SortBy = null,
    bool Ascending = true
) : ICachedQuery<PagedResponse<SkillDto>>
{
    public TimeSpan? Duration => TimeSpan.FromHours(12);
    public string Key => nameof(GetAllSkills);
}
