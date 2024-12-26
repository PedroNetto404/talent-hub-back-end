using System.Reflection;
using MediatR;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Behaviors;

public sealed class CacheBehavior<TQuery, TResult>(
    ICacheProvider cacheProvider,
    IUserContext userContext,
    IHasher hasher
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
        if (GetCacheType() is not Type cacheType) 
        {
            return await next();
        }

        string key = GenerateCacheKey(request);

        object? cachedValue = await GetCachedValueAsync(cacheType, key, cancellationToken);
        if (cachedValue is not null)
        {
            return CreateResultFromCache(cachedValue);
        }

        TResult result = await next();
        if (result.IsFail)
        {
            return result;
        }

        await CacheResultAsync(
            request,
            key,
            result,
            cancellationToken
        );

        return result;
    }

    private Type? GetCacheType() => 
        typeof(TResult).GetGenericArguments().FirstOrDefault();

    private string GenerateCacheKey(TQuery request) => 
        hasher.Hash($"{(request.Scoped ? userContext.UserId : string.Empty)}{request.Key}");

    private async Task<object?> GetCachedValueAsync(
        Type cacheType, 
        string key, 
        CancellationToken cancellationToken
    )
    {
        MethodInfo getCacheMethod = typeof(ICacheProvider)
            .GetMethod(nameof(ICacheProvider.GetAsync))!
            .MakeGenericMethod(cacheType);

        object cachedValueTask = getCacheMethod.Invoke(
            cacheProvider, 
            [key, cancellationToken]
        )!;
        await (Task)cachedValueTask;

        return cachedValueTask
            .GetType()
            .GetProperty("Result")!
            .GetValue(cachedValueTask, null);
    }

    private TResult CreateResultFromCache(object cachedValue)
    {
        if (cachedValue is IPageResponse pageResponse)
        {
            cachedValue = pageResponse.FromCache();
        }

        ConstructorInfo constructor = typeof(Result<>)
            .MakeGenericType(cachedValue.GetType())
            .GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                null,
                new Type[] { cachedValue.GetType() },
                null
            )!;

        return (TResult)constructor.Invoke([cachedValue]);
    }

    private async Task CacheResultAsync(
        TQuery request, 
        string key,
        TResult result, 
        CancellationToken cancellationToken
    )
    {
        object value = result.GetType().GetProperty("Value")!.GetValue(result)!;
        await cacheProvider.SetAsync(
            key,
            value,
            request.Duration,
            cancellationToken
        );
    }
}
