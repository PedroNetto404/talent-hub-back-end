using System;
using System.Linq.Expressions;
using System.Reflection;
using Humanizer;

namespace TalentHub.ApplicationCore.Utils;

public static class SortingUtil
{
    public static Expression<Func<T, object?>> CreateOrderExpression<T>(string propertyName)
    {
        Type aggregateType = typeof(T);

        string normalizedPropertyName = propertyName.Pascalize();
        PropertyInfo property = aggregateType.GetProperty(normalizedPropertyName)
        ?? throw new InvalidOperationException($"Property {normalizedPropertyName} not found in {aggregateType.Name}");

        ParameterExpression parameter = Expression.Parameter(aggregateType, "x");
        MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, property);
  
        var orderByExpression = Expression.Lambda<Func<T, object?>>(Expression.Convert(propertyAccess, typeof(object)), parameter);
     
        return orderByExpression;
    }
}
