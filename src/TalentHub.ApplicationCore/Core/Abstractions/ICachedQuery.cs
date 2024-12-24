using System.Text.Json.Serialization;
using TalentHub.ApplicationCore.Extensions;

namespace TalentHub.ApplicationCore.Core.Abstractions;

public interface ICachedQuery
{
    public TimeSpan Duration { get; }
    public string Key { get; }
    public bool Scoped { get; }
}

public abstract record CachedQuery<TResult> :
    ICachedQuery,
    IQuery<TResult>
    where TResult : notnull
{
    [JsonIgnore]
    public virtual TimeSpan Duration => TimeSpan.FromSeconds(30);

    [JsonIgnore]
    public string Key => $"{GetType().Name}:{this.ToJson()}";

    [JsonIgnore]
    public virtual bool Scoped => false;
}
