using Microsoft.Extensions.Caching.Memory;
using TalentHub.ApplicationCore.Ports;

namespace TalentHub.Infra.Cache;

public sealed class InMemoryCacheProvider(
    IMemoryCache memoryCache
) : ICacheProvider
{
    public Task<T?> GetAsync<T>(
        string key, 
        CancellationToken cancellationToken = default
    ) => Task.FromResult(memoryCache.Get<T>(key));

    public Task RemoveAsync(
        string key, 
        CancellationToken cancellationToken = default
    )
    {
        memoryCache.Remove(key);
        return Task.CompletedTask;
    }

    public Task SetAsync<T>(
        string key, 
        T value, 
        TimeSpan? expiration = null, 
        CancellationToken cancellationToken = default
    )
    {
        memoryCache.Set(
            key, 
            value,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = 
                    DateTimeOffset.UtcNow.Add(expiration 
                    ?? TimeSpan.FromSeconds(30)),
                
            }
        );

        return Task.CompletedTask;
    }
}
