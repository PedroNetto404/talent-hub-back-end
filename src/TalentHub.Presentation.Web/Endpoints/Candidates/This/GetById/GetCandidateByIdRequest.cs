using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.This.GetById;

public sealed record GetCandidateByIdRequest(
    [property: FromRoute(Name = "candidateId")] Guid CandidateId
);
