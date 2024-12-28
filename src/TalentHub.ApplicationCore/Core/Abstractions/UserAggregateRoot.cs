namespace TalentHub.ApplicationCore.Core.Abstractions;

public abstract class UserAggregateRoot : AuditableAggregateRoot
{
  public Guid UserId { get; protected set; }
}
