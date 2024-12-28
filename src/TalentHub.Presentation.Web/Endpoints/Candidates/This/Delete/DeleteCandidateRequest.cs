using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.This.Delete;

public sealed record DeleteCandidateRequest(
    [property: FromRoute(Name = "candidateId")] Guid CandidateId
);
