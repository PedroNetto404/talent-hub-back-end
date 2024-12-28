using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Ports;

public interface IResourceOwnershipValidator<T>
    where T : UserAggregateRoot
{
    Task<Result> ValidateOwnershipAsync(Guid resourceId, CancellationToken ct = default);
}

