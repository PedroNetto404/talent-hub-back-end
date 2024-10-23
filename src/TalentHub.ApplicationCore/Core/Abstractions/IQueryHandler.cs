using MediatR;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Core.Abstractions;

public interface IQueryHandler<TQuery, TResult>
    : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>
    where TResult : notnull;