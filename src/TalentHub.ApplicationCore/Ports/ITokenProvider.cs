using TalentHub.ApplicationCore.Resources.Users;
using TalentHub.ApplicationCore.Resources.Users.ValueObjects;

namespace TalentHub.ApplicationCore.Ports;

public interface ITokenProvider
{
    Token GenerateTokenFor(User user);
    Token GenerateRefreshToken(User user);
}
