using MediatR;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Shared.Dtos;

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

        if (cached is null)
        {
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
        
        if(cached is Result<PageResponse> { IsOk: true, Value: var cachedPage})
        {

            return Result.Ok(cachedPage.FromCache()) as TResult;
        }

        return cached;
    }
}
