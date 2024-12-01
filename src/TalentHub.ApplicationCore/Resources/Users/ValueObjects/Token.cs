using TalentHub.ApplicationCore.Ports;

namespace TalentHub.ApplicationCore.Resources.Users.ValueObjects;

public sealed record Token(
    string Value,
    long Expiration
)
{
    public bool IsExpired(IDateTimeProvider dateTimeProvider) =>
        dateTimeProvider.UtcNow > DateTimeOffset.FromUnixTimeSeconds(Expiration);
}
