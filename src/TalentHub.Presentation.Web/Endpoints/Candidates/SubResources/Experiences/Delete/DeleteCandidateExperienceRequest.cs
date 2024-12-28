using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.Experiences.Delete;

public sealed record DeleteCandidateExperienceRequest(
    [property: FromRoute(Name = "candidateId")]
    Guid CandidateId,
    [property: FromRoute(Name = "experienceId")]
    Guid ExperienceId
);
