using System;
using Ardalis.Specification;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Resources.Skills.Specs;

public sealed class GetSkillsSpec : GetPageSpec<Skill>
{
    public GetSkillsSpec(
        int limit = int.MaxValue,
        int offset = 0,
        string? sortBy = null,
        bool ascending = true
    ) : base(
        limit,
        offset,
        sortBy,
        ascending
    )
    { }

    public GetSkillsSpec(
        IEnumerable<Guid> ids,
        int limit = int.MaxValue,
        int offset = 0,
        string? sortBy = null,
        bool ascending = true
    ) : this(
        limit,
        offset,
        sortBy,
        ascending
    )
    {
        if (ids.Any())
        {
            Query.Where(p => ids.Contains(p.Id));
        }
    }
}
