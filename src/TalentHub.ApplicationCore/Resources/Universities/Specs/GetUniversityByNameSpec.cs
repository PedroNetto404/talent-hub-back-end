using System;
using Ardalis.Specification;
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
        bool ascending = true
    ) : base(limit, offset, sortBy, ascending) { }

    public GetUniversitiesSpec(
        IEnumerable<Guid> ids,
        int limit,
        int offset,
        string? sortBy = null,
        bool ascending = true
    ) : this(limit, offset, sortBy, ascending)
    {
        if (ids.Any())
        {
            Query.Where(u => ids.Contains(u.Id));
        }
    }
}
