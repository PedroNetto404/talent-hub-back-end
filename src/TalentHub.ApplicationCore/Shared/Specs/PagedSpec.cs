using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Shared.Specs;

public abstract class PagedSpec<T> : Specification<T>
    where T : AggregateRoot
{
    protected PagedSpec(
        int limit,
        int offset
    ) => 
        Query
        .Skip(offset)
        .Take(limit);
}