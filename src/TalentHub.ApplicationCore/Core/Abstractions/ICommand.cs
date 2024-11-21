namespace TalentHub.ApplicationCore.Core.Abstractions;

public interface ICommand :
    ICommandBase,
    IUseCaseInput;

public interface ICommand<TResult> :
    ICommandBase,
    IUseCaseInput<TResult>
    where TResult : notnull;