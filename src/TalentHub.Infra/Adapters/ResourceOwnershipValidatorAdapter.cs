using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;

namespace TalentHub.Infra.Adapters;

public sealed class ResourceOwnershipValidatorAdapter<T>(
    IRepository<T> repository,
    IUserContext userContext
) : IResourceOwnershipValidator<T>
    where T : UserAggregateRoot
{
    public async Task<Result> ValidateOwnershipAsync(
        Guid resourceId, 
        CancellationToken ct = default
    )
    {
        Guid? userId = userContext.UserId;
        if(userId is null)
        {
            return Error.Unauthorized();
        }

        T? resource = await repository.GetByIdAsync(resourceId, ct);
        if(resource is null)
        {
            return Error.NotFound($"{typeof(T).Name}");
        }

        if(resource.UserId != userId)
        {
            return Error.Forbiden();
        }

        return Result.Ok();
    }   
}
