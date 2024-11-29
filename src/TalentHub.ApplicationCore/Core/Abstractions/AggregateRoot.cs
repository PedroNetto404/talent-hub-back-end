namespace TalentHub.ApplicationCore.Core.Abstractions;

public abstract class AggregateRoot : Entity, IAggregateRoot
{
    private readonly List<IDomainEvent> _events = [];

    public IEnumerable<IDomainEvent> Events => [.. _events];

    public void ClearEvents() => _events.Clear();

    public void RaiseEvent(IDomainEvent domainEvent) => _events.Add(domainEvent);
}
