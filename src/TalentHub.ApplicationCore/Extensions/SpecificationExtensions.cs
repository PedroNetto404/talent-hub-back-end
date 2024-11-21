using Ardalis.Specification;
using TalentHub.ApplicationCore.Utils;

namespace TalentHub.ApplicationCore.Extensions;

public static class SpecificationExtensions
{
    public static void Sort<T>(this ISpecificationBuilder<T> query, string propertyName, bool ascending)
    {
        var orderByExpression = SortingUtil.CreateOrderExpression<T>(propertyName);

        if (ascending) query.OrderBy(orderByExpression);
        else query.OrderByDescending(orderByExpression);
    }
}

