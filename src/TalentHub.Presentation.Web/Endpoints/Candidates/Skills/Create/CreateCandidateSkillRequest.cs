using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Skills.Create;

public sealed record CreateCandidateSkillRequest(
    [property: FromRoute(Name = "candidateId")] Guid CandidateId,
    Guid SkillId,
    string Proficiency
);
