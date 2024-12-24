using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Skills.Delete;

public sealed record DeleteCandidateSkillRequest(
    [property: FromRoute(Name = "candidateId")] Guid CandidateId,
    [property: FromRoute(Name = "candidateSkillId")] Guid CandidateSkillId
);
