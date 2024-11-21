using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Candidates.Entities;

public sealed class AcademicExperience : Experience
{
    private AcademicExperience(
        DatePeriod start,
        DatePeriod? end,
        bool isCurrent,
        EducationLevel level,
        ProgressStatus status,
        Guid courseId,
        Guid institutionId) : base(start, end, isCurrent)
    {
        Level = level;
        Status = status;
        CourseId = courseId;
        InstitutionId = institutionId;
    }

    // public static Result<AcademicExperience> Create(
    //     DatePeriod start,
    //     DatePeriod? end,
    //     bool isCurrent,
    //     EducationLevel level,
    //     ProgressStatus status,
    //     Guid courseId,
    //     Guid institutionId
    // )
    // {
    //     if(courseId == Guid.Empty) return new Error("candidate", $"cannot create academic experience with courseId {courseId}");
    //     if(institutionId == Guid.Empty) return new Error()
    // }

#pragma warning disable CS0628 // New protected member declared in sealed type
    protected AcademicExperience() { }
#pragma warning restore CS0628 // New protected member declared in sealed type

    public EducationLevel Level { get; private set; }
    public ProgressStatus Status { get; private set; }
    public Guid CourseId { get; private set; }
    public Guid InstitutionId { get; private set; }

    public void UpdateStatus(ProgressStatus status) =>
        Status = status;
}
