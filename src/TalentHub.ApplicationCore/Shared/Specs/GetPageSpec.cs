using System;
using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Extensions;

namespace TalentHub.ApplicationCore.Shared.Specs;

public class GetPageSpec<T> : Specification<T> where T : AggregateRoot
{
    public GetPageSpec
    (
        int limit,
        int offset,
        string? sortBy = null,
        bool ascending = true
    )
    {
        Query
            .Skip(offset)
            .Take(limit);

        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            Query.Sort(sortBy, ascending);
        }

        Query.AsNoTracking();
    }
}
