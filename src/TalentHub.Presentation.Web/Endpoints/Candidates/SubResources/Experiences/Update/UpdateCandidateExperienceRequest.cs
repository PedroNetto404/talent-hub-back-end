using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.Experiences.Update;

public sealed record UpdateCandidateExperienceRequest(
    [property: FromRoute(Name = "candidateId")] Guid CandidateId,
    [property: FromRoute(Name = "experienceId")] Guid ExperienceId,
    string Type,
    DatePeriod Start,
    DatePeriod? End,
    bool IsCurrent,
    IEnumerable<string> Activities,
    IEnumerable<string>? AcademicEntities,
    int? CurrentSemester,
    string? Status,
    string? Description
);
