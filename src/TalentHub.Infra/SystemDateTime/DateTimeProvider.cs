using TalentHub.ApplicationCore.Ports;

namespace TalentHub.Infra.SystemDateTime;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
