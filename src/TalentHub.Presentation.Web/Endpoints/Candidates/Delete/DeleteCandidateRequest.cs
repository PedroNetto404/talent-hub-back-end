using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Delete;

public sealed record DeleteCandidateRequest(
    [property: FromRoute(Name = "candidateId")] Guid CandidateId
);
