using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Create;

public sealed record CreateCandidateRequest(
    string Name,
    string? Summary,
    bool AutoMatchEnabled,
    string Phone,
    DateOnly BirthDate,
    decimal? ExpectedRemuneration,
    string? InstagramUrl,
    string? LinkedInUrl,
    string? GitHubUrl,
    string? WebsiteUrl,
    Address Address,
    IEnumerable<string> Hobbies,
    IEnumerable<string> DesiredWorkplaceTypes,
    IEnumerable<string> DesiredJobTypes
);
