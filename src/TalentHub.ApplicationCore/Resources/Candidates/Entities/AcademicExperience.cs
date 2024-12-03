using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates.Entities;

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
        var result = Result.FailEarly(
            () => Result.FailIf(end != null && start > end, "start date must be less than end date"),
            () => Result.FailIf(Guid.Empty == courseId, "invalid course id"),
            () => Result.FailIf(Guid.Empty == institutionId, "invalid university id"),
            () => Result.FailIf(currentSemester < 0, "current semester must be greater than 0")
        );

        if(result.IsFail)
        {
            return result.Error;
        }

        return new AcademicExperience(
            start, 
            end,
            currentSemester,
            isCurrent,
            level,
            status,
            courseId,
            institutionId);
    }

#pragma warning disable CS0628 // New protected member declared in sealed type
    protected AcademicExperience()
    {
    }
#pragma warning restore CS0628 // New protected member declared in sealed type

    private readonly List<AcademicEntity> _academicEntities = [];

    public int CurrentSemester { get; private set; }
    public DatePeriod ExpectedGraduation { get; private set; }
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
        {
            return Error.BadRequest("academic entity already added");
        }

        _academicEntities.Add(academicEntity);
        return Result.Ok();
    }

    public void ClearAcademicEntities() => _academicEntities.Clear();

    public Result UpdateCurrentSemester(int currentSemester)
    {
        if (currentSemester < 1)
        {
            return Error.BadRequest("current semester must be greater than 0");
        }

        CurrentSemester = currentSemester;
        return Result.Ok();
    }

    public void UpdateExpectedGraduation(DatePeriod expectedGraduation) =>
        ExpectedGraduation = expectedGraduation;
}
