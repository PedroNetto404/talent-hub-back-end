using System;
using Ardalis.Specification;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Resources.Universities.Specs;

public sealed class GetUniversityByNameSpec : Specification<University>
{
    public GetUniversityByNameSpec(string name)
    {
        Query.Where(u => u.Name == name).AsNoTracking();
    }
}

public sealed class GetUniversitiesSpec : GetPageSpec<University>
{
    public GetUniversitiesSpec(
        int limit,
        int offset,
        string? sortBy = null,
        SortOrder sortOrder = SortOrder.Ascending
    ) : base(limit, offset, sortBy, sortOrder) { }

    public GetUniversitiesSpec(
        IEnumerable<Guid> ids,
        int limit,
        int offset,
        string? sortBy = null,
        SortOrder sortOrder = SortOrder.Ascending
    ) : this(limit, offset, sortBy, sortOrder)
    {
        if (ids.Any())
        {
            Query.Where(u => ids.Contains(u.Id));
        }
    }
}
