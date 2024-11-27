using Ardalis.Specification;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Skills.Specs;

public sealed class GetAllSkillsSpec : PagedSpec<Skill>
{
    public GetAllSkillsSpec(
        int limit,
        int offset,
        string? sortBy,
        bool ascending = true,
        params Guid[] ids) : base(
        limit,
        offset,
        sortBy,
        ascending)
    {
        if (ids.Length > 0) Query.Where(skill => ids.Contains(skill.Id));
        Query.AsNoTracking();
    }
}