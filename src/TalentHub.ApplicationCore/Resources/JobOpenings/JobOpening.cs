using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.JobOpenings;

public sealed class JobOpening : AuditableAggregateRoot
{
    public Guid CompanyId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
}
