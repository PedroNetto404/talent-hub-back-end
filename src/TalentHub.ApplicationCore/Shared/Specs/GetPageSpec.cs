using System;
using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.ApplicationCore.Shared.Specs;

public class GetPageSpec<T> : Specification<T> where T : AggregateRoot
{
    public GetPageSpec
    (
        int limit,
        int offset,
        string? sortBy = null,
        SortOrder sortOrder = SortOrder.Ascending
    )
    {
        Query
            .Skip(offset)
            .Take(limit);

        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            Query.Sort(sortBy, sortOrder);
        }

        Query.AsNoTracking();
    }
}
