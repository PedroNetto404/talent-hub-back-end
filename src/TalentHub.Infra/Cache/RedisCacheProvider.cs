using System.Text.Json;
using StackExchange.Redis;
using TalentHub.ApplicationCore.Ports;

namespace TalentHub.Infra.Cache;

public sealed class RedisCacheProvider(
    IConnectionMultiplexer connectionMultiplexer
) : ICacheProvider
{
    private readonly IDatabase _database =
        connectionMultiplexer.GetDatabase();

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        RedisValue redisValue = await _database.StringGetAsync(key);

        if (redisValue.IsNullOrEmpty)
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(redisValue!);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default) =>
         _database.KeyDeleteAsync(key);

    public async Task SetAsync(
        string key, 
        object value, 
        TimeSpan? expiration = null, 
        CancellationToken cancellationToken = default
    )
    {
        string json = JsonSerializer.Serialize(value);

        if (expiration.HasValue)
        {
            await _database.StringSetAsync(key, json, expiration);
        }
        else
        {
            await _database.StringSetAsync(key, json);
        }
    }
}
