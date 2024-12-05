using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Extensions;

public static class RepositoryExtensions
{
    public static Task<List<T>> GetManyByIdsAsync<T>(
           this IRepository<T> repository,
           IEnumerable<Guid> ids,
           CancellationToken cancellationToken = default)
           where T : AggregateRoot =>
           repository.ListAsync(new GetManySpec<T>([.. ids]), cancellationToken);

    public static Task<List<T>> GetPageAsync<T>(
        this IRepository<T> repository,
        int limit = 10,
        int offset = 0,
        string? sortBy = null,
        bool ascending = true,
        Action<ISpecificationBuilder<T>>? additionalSpec = null,
        CancellationToken cancellationToken = default
    ) where T : AggregateRoot =>
        repository.ListAsync(
            new GetPageSpec<T>(
                limit, 
                offset, 
                sortBy, 
                ascending, 
                additionalSpec
            ),
            cancellationToken
        );

    public static Task<int> CountAsync<T>(
       this IRepository<T> repository,
       Action<ISpecificationBuilder<T>>? additionalSpec = null,
       CancellationToken cancellationToken = default
   ) where T : AggregateRoot =>
        repository.CountAsync(
            new GetPageSpec<T>(
                int.MaxValue, 
                0, 
                null, 
                true, 
                additionalSpec
            ),
            cancellationToken);

    public static Task<T?> FirstOrDefaultAsync<T>(
        this IRepository<T> repository,
        Action<ISpecificationBuilder<T>> additionalSpec,
        CancellationToken cancellationToken = default
    ) where T : AggregateRoot =>
        repository.FirstOrDefaultAsync(
            new Spec<T>(additionalSpec), 
            cancellationToken);

    public static Task<List<T>> ListAsync<T>(
        this IRepository<T> repository,
        Action<ISpecificationBuilder<T>> additionalSpec,
        CancellationToken cancellationToken = default
    ) where T : AggregateRoot =>
        repository.ListAsync(
            new Spec<T>(additionalSpec),
            cancellationToken
        );

    private class Spec<T> : Specification<T> where T : AggregateRoot
    {
        public Spec(Action<ISpecificationBuilder<T>> additionalSpec) => 
            additionalSpec(Query);
    }

    private class GetPageSpec<T> : Specification<T> where T : AggregateRoot
    {
        public GetPageSpec(
            int limit,
            int offset,
            string? sortBy,
            bool ascending,
            Action<ISpecificationBuilder<T>>? additionalSpec = null
        )
        {
            Query.Paginate(limit, offset);

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                Query.Sort(sortBy, ascending);
            }

            additionalSpec?.Invoke(Query);
        }
    }

    private class GetManySpec<T> : Specification<T> where T : AggregateRoot
    {
        public GetManySpec(params Guid[] ids) => 
            Query.Where(x => ids.Contains(x.Id));
    }
}
