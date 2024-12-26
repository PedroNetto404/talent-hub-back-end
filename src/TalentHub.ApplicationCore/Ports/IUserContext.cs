using TalentHub.ApplicationCore.Resources.Users;

namespace TalentHub.ApplicationCore.Ports;

public interface IUserContext 
{
    Guid? UserId { get; }
    Task<User?> GetCurrentAsync(CancellationToken cancellationToken);

    bool IsCompany { get; }

    bool IsCandidate { get; }

    bool IsAdmin { get; }
}
