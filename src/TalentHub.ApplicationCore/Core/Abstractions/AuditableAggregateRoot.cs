namespace TalentHub.ApplicationCore.Core.Abstractions;

public abstract class AuditableAggregateRoot : AggregateRoot
{
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAtUtc { get; private set; } = DateTime.UtcNow;
    public DateTime? DeletedAtUtc { get; private set; }

    protected void OnDelete() => DeletedAtUtc = DateTime.UtcNow;
    protected void OnUpdate() => UpdatedAtUtc = DateTime.UtcNow;
}
