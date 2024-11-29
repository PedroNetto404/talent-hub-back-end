using TalentHub.ApplicationCore.Users;
using TalentHub.ApplicationCore.Users.ValueObjects;

namespace TalentHub.ApplicationCore.Ports;

public interface ITokenProvider
{
    Token GenerateTokenFor(User user);
    Token GenerateRefreshToken(User user);
}
