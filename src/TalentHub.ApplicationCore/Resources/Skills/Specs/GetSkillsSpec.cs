using System;
using Ardalis.Specification;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Resources.Skills.Specs;

public sealed class GetSkillsSpec : GetPageSpec<Skill>
{
    public GetSkillsSpec(
        int limit = int.MaxValue,
        int offset = 0,
        string? sortBy = null,
        SortOrder sortOrder = SortOrder.Ascending
    ) : base(
        limit,
        offset,
        sortBy,
        sortOrder
    )
    { }

    public GetSkillsSpec(
        IEnumerable<Guid> ids,
        int limit = int.MaxValue,
        int offset = 0,
        string? sortBy = null,
        SortOrder sortOrder = SortOrder.Ascending
    ) : this(
        limit,
        offset,
        sortBy,
        sortOrder
    )
    {
        if (ids.Any())
        {
            Query.Where(p => ids.Contains(p.Id));
        }
    }
}
