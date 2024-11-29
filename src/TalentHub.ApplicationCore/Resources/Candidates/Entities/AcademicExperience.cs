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
        int currentSemester,
        bool isCurrent,
        EducationLevel level,
        ProgressStatus status,
        Guid courseId,
        Guid universityId) : base(start, end, isCurrent)
    {
        Level = level;
        Status = status;
        CourseId = courseId;
        UniversityId = universityId;
        CurrentSemester = currentSemester;
    }

    public static Result<AcademicExperience> Create(
        DatePeriod start,
        DatePeriod? end,
        int currentSemester,
        bool isCurrent,
        EducationLevel level,
        ProgressStatus status,
        Guid courseId,
        Guid institutionId)
    {
        if (end != null && start > end) return new Error("experience", "Start date must be less than end date.");
        if (Guid.Empty == courseId) return new Error("experience", "CourseId must be provided.");
        if (Guid.Empty == institutionId) return new Error("experience", "InstitutionId must be provided.");

        return Result.Ok(
            new AcademicExperience(
                start, 
                end, 
                currentSemester, 
                isCurrent, 
                level, 
                status, 
                courseId,
                institutionId));
    }


#pragma warning disable CS0628 // New protected member declared in sealed type
    protected AcademicExperience()
    {
    }
#pragma warning restore CS0628 // New protected member declared in sealed type

    private readonly List<AcademicEntity> _academicEntities = [];

    public int CurrentSemester { get; private set; }
    public EducationLevel Level { get; private set; }
    public ProgressStatus Status { get; private set; }
    public StudyPeriod Period { get; private set; }
    public Guid CourseId { get; private set; }
    public Guid UniversityId { get; private set; }
    public IReadOnlyList<AcademicEntity> AcademicEntities => _academicEntities.AsReadOnly();

    public void UpdateStatus(ProgressStatus status) =>
        Status = status;

    public Result AddAcademicEntity(AcademicEntity academicEntity)
    {
        if (_academicEntities.Contains(academicEntity))
            return new Error("academic_experience", "academic entity already added");

        _academicEntities.Add(academicEntity);
        return Result.Ok();
    }

    public void ClearAcademicEntities() => _academicEntities.Clear();

    public Result UpdateCurrentSemester(int currentSemester)
    {
        if (currentSemester < 1)
        {
            return new Error("academic_experience", "Current semester must be greater than 0");
        }
        
        CurrentSemester = currentSemester;
        return Result.Ok();
    }
}