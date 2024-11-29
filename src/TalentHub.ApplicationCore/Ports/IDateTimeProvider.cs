namespace TalentHub.ApplicationCore.Ports;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
