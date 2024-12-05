using System.Linq.Expressions;
using Ardalis.Specification;
using TalentHub.ApplicationCore.Utils;

namespace TalentHub.ApplicationCore.Extensions;

public static class SpecificationExtensions
{
    public static void Sort<T>(this ISpecificationBuilder<T> query, string propertyName, bool ascending)
    {
        Expression<Func<T, object?>> orderByExpression = SortingUtil.CreateOrderExpression<T>(propertyName);

        if (ascending)
        {
            query.OrderBy(orderByExpression);
            return;
        }

        query.OrderByDescending(orderByExpression);
    }

    public static ISpecificationBuilder<T> Paginate<T>(this ISpecificationBuilder<T> query, int limit, int offset) =>
        query.Skip(offset).Take(limit);
}
