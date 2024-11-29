using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Candidates.Events;

public sealed record CandidateCreatedEvent(Guid CandidateId) : IDomainEvent;
