namespace TalentHub.ApplicationCore.Core.Abstractions;

public interface IQuery<TResult> :
    IQueryBase,
    IUseCaseInput<TResult>
    where TResult : notnull;

