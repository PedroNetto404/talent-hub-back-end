namespace TalentHub.ApplicationCore.Candidates.Dtos;

public sealed record AcademicExperienceDto(
    string StartMonth,
    string StartYear,
    string? EndMonth,
    string? EndYear,
    bool IsCurrent,
    string Level,
    string Status,
    Guid CourseId,
    Guid InstitutionId
); 

