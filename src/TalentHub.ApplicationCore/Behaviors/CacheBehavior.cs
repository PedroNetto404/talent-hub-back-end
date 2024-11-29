using MediatR;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;

namespace TalentHub.ApplicationCore.Behaviors;

public sealed class CacheBehavior<TQuery, TResult>(
    ICacheProvider cacheProvider
) :
    IPipelineBehavior<TQuery, TResult>
    where TQuery : ICachedQuery
    where TResult : Result
{
    public async Task<TResult> Handle(
        TQuery request,
        RequestHandlerDelegate<TResult> next,
        CancellationToken cancellationToken)
    {
        TResult? cached = await cacheProvider.GetAsync<TResult>(
            request.Key,
            cancellationToken
        );

        if (cached is not null)
        { return cached; }

        TResult result = await next();
        if (result.IsOk)
        {
            await cacheProvider.SetAsync(
                request.Key, 
                result, 
                request.Duration, 
                cancellationToken
            );
        }

        return result;
    }
}
