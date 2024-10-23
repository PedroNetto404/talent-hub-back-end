namespace TalentHub.ApplicationCore.Core.Abstractions;

public interface ICachedQuery<TResult> : IQuery<TResult> where TResult : notnull
{
    public TimeSpan? Duration { get; }
    public string Key { get; }
}
