using System.Reflection;
using MediatR;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Behaviors;

public sealed class CacheBehavior<TQuery, TResult>(
    ICacheProvider cacheProvider,
    IUserContext userContext
) : IPipelineBehavior<TQuery, TResult>
    where TQuery : ICachedQuery
    where TResult : Result
{
    public async Task<TResult> Handle(
        TQuery request,
        RequestHandlerDelegate<TResult> next,
        CancellationToken cancellationToken
    )
    {
        Type? cacheType = typeof(TResult).GetGenericArguments().FirstOrDefault();
        if (cacheType == null)
        {
            return await next();
        }

        string key = $"{(request.Scoped ? userContext.UserId : string.Empty)}{request.Key}";

        MethodInfo getCacheMethod =
            typeof(ICacheProvider)
                .GetMethod(nameof(ICacheProvider.GetAsync))!
                .MakeGenericMethod(cacheType);

        object cachedValueTask = getCacheMethod.Invoke(cacheProvider, [key, cancellationToken])!;
        await (Task)cachedValueTask;

        object? cachedValue =
            cachedValueTask
                .GetType()
                .GetProperty("Result")!
                .GetValue(cachedValueTask, null);
        if (cachedValue is not null)
        {
            if (cachedValue is PageResponse pageResponse)
            {
                cachedValue = pageResponse.FromCache();
            }

            ConstructorInfo constructor = typeof(Result<>)
                .MakeGenericType(cacheType)
                .GetConstructor(
                   BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                    null,
                    [cacheType],
                    null
                )!;

            return (TResult)constructor.Invoke([cachedValue]);
        }

        TResult result = await next();
        if (result.IsFail)
        {
            return result;
        }

        object value = result.GetType().GetProperty("Value")!.GetValue(result)!;
        await cacheProvider.SetAsync(
            key,
            value,
            request.Duration,
            cancellationToken
        );

        return result;
    }
}
