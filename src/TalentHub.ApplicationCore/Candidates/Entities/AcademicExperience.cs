using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.ApplicationCore.Candidates.Entities;

public sealed class AcademicExperience : Experience
{
    public EducationLevel Level { get; private set; }
    public ProgressStatus Status { get; private set; }
    public Guid CourseId { get; private set; }
    public Guid InstitutionId { get; private set; }
    
    public void UpdateStatus(ProgressStatus status) =>
        Status = status;
}
