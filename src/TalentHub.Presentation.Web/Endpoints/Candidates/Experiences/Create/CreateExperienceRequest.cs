using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Experiences.Create;

public sealed record CreateExperienceRequest(
    [FromRoute(Name = "candidateId")] Guid CandidateId,
    [FromRoute(Name = "type")] string Type,
    [FromBody] DatePeriod Start,
    [FromBody] DatePeriod? End,
    [FromBody] bool IsCurrent,
    [FromBody] IEnumerable<string> Activities,
    [FromBody] DatePeriod? ExpectedGraduation,
    [FromBody] string? EducationalLevel,
    [FromBody] string? ProgressStatus,
    [FromBody] int? CurrentSemester,
    [FromBody] IEnumerable<string>? AcademicEntities,
    [FromBody] Guid? CourseId,
    [FromBody] Guid? UniversityId,
    [FromBody] string? Position,
    [FromBody] string? Description,
    [FromBody] string? Company,
    [FromBody] string? ProfessionalLevel
);
