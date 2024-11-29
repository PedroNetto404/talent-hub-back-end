namespace TalentHub.ApplicationCore.Core.Abstractions;

public interface ICachedQuery 
{
    public TimeSpan? Duration { get; }
    public string Key { get; }
}

public interface ICachedQuery<TResult> : 
    ICachedQuery,
    IQuery<TResult> 
    
    where TResult : notnull;
