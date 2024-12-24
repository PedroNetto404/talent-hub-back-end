using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Skills.Update;

public sealed record UpdateCandidateSkillRequest(
    [property: FromRoute(Name = "candidateId")] Guid CandidateId,
    [property: FromRoute(Name = "candidateSkillId")] Guid CandidateSkillId,
    [property: FromBody] string Proficiency
);
