using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Users;

namespace TalentHub.ApplicationCore.Ports;

public interface IUserContext 
{
    Guid UserId { get; }
    Task<Result<User>> GetCurrentAsync(CancellationToken cancellationToken);
}
