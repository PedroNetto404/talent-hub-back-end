using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.This.Update;

public sealed record UpdateCandidateRequest(
    [property: FromRoute(Name = "candidateId")] Guid CandidateId,
    string Name,
    string? Summary,
    bool AutoMatchEnabled,
    string Phone,
    Address Address,
    decimal? ExpectedRemuneration,
    string? InstagramUrl,
    string? LinkedInUrl,
    string? GitHubUrl,
    string? WebsiteUrl,
    IEnumerable<string> DesiredJobTypes,
    IEnumerable<string> DesiredWorkplaceTypes,
    IEnumerable<string> Hobbies
);
