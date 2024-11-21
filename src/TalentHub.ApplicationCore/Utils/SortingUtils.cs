using System;
using System.Linq.Expressions;
using Humanizer;

namespace TalentHub.ApplicationCore.Utils;

public static class SortingUtil
{
    public static Expression<Func<T, object?>> CreateOrderExpression<T>(string propertyName)
    {
        var aggregateType = typeof(T);

        var normalizedPropertyName = propertyName.Pascalize();
        var property = aggregateType.GetProperty(normalizedPropertyName)
        ?? throw new InvalidOperationException($"Property {normalizedPropertyName} not found in {aggregateType.Name}");

        var parameter = Expression.Parameter(aggregateType, "x");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
  
        var orderByExpression = Expression.Lambda<Func<T, object?>>(Expression.Convert(propertyAccess, typeof(object)), parameter);
     
        return orderByExpression;
    }
}