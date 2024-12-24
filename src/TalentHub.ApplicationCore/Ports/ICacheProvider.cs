namespace TalentHub.ApplicationCore.Ports;

public interface ICacheProvider 
{
    Task<T?> GetAsync<T>(
        string key, 
        CancellationToken cancellationToken = default
    );

    Task SetAsync(
        string key,
        object value,    
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default
    );

    Task RemoveAsync(
        string key, 
        CancellationToken cancellationToken = default
    );
}
