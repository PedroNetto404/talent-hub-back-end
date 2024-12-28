using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.Skills.Create;

public sealed record CreateCandidateSkillRequest(
    [property: FromRoute(Name = "candidateId")] Guid CandidateId,
    Guid SkillId,
    string Proficiency
);
