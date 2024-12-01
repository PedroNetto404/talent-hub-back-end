using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Candidates.Events;

public sealed record CandidatePhoneUpdatedEvent(Guid CandidateId) : IDomainEvent;
