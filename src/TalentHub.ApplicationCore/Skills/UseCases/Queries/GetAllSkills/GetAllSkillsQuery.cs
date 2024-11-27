using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Skills.Dtos;

namespace TalentHub.ApplicationCore.Skills.UseCases.Queries.GetAllSkills;

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