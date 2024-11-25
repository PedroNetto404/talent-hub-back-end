namespace TalentHub.ApplicationCore.Core.Abstractions;

public interface IAggregateRoot 
{
    void RaiseEvent(IDomainEvent domainEvent); 

    void ClearEvents();

    IEnumerable<IDomainEvent> Events { get; }
}
