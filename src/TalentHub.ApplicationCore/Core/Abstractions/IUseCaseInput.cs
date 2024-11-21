using MediatR;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Core.Abstractions;

public interface IUseCaseInput : IRequest<Result>;

public interface IUseCaseInput<T> : IRequest<Result<T>> where T : notnull;

