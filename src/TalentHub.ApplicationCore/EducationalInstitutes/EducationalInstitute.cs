using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.EducationalInstitutes;

public sealed class EducationalInstitute : AggregateRoot
{
    public string Name { get; private set; }
}