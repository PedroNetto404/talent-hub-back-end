using MediatR;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Core.Abstractions;

public interface ICommand : 
    ICommandBase, 
    IRequest<Result>;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;