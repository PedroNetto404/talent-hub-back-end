
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Update;

public sealed record UpdateCandidateRequest(
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
