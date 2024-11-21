using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Skills.UseCases.Dtos;

namespace TalentHub.ApplicationCore.Skills.UseCases.Queries.GetAllSkills;

public sealed record GetAllSkillsQuery(
    int Limit,
    int Offset,
    string? SortBy = null,
    bool SortAscending = true
) : ICachedQuery<PagedResponse<SkillDto>>
{
    public TimeSpan? Duration => TimeSpan.FromHours(12);
    public string Key => nameof(GetAllSkills);
}
