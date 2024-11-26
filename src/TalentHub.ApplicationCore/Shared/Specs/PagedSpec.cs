using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Extensions;

namespace TalentHub.ApplicationCore.Shared.Specs;

public abstract class PagedSpec<T> : Specification<T>
    where T : AggregateRoot
{
    protected PagedSpec(
        int limit, 
        int offset, 
        string? sortBy = null, 
        bool ascending = true)
    {
        Query.Skip(offset);
        Query.Take(limit);

        if (sortBy is not null) Query.Sort(sortBy, ascending);
    }
}