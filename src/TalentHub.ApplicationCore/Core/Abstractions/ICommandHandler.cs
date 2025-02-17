using MediatR;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Core.Abstractions;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

public interface ICommandHandler<TCommand, TResult> :
    IRequestHandler<TCommand, Result<TResult>>
    where TCommand : ICommand<TResult>
    where TResult : notnull;