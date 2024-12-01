using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Users;

namespace TalentHub.ApplicationCore.Ports;

public interface IUserContext 
{
    Task<Result<User>> GetCurrentAsync();
}
