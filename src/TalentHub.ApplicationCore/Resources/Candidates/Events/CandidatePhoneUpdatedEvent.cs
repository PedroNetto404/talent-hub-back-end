using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Candidates.Events;

public sealed record CandidatePhoneUpdatedEvent(Guid CandidateId) : IDomainEvent;