using MediatR;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Core.Abstractions;

public interface IQuery<TResult> :
    IQueryBase,
    IRequest<Result<TResult>>
    where TResult : notnull;

public interface ICommand<TResult> : 
    ICommandBase, 
    IRequest<Result<TResult>> 
    where TResult : notnull;