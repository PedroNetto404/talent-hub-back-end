using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Applications.Enums;

namespace TalentHub.ApplicationCore.Resources.Applications;

public sealed class JobApplication : AuditableAggregateRoot
{
    public Guid JobOpportunityId { get; private set; }      
    public Guid CandidateId { get; private set; }
    public ApplicationStatus Status { get; private set; }
}
