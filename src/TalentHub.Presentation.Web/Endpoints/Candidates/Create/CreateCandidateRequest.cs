namespace TalentHub.Presentation.Web.Endpoints.Candidates.Create;

public sealed record CreateCandidateRequest
{
    public string Name { get; init; }
    public string? Summary { get; init; }
    public bool AutoMatchEnabled { get; init; }
    public string Phone { get; init; }
    public DateOnly BirthDate { get; init; }
    public decimal ExpectedRemuneration { get; init; }
    public string? InstagramUrl { get; init; }
    public string? LinkedInUrl { get; init; }
    public string? GitHubUrl { get; init; }
    public string? WebsiteUrl { get; init; }
    public string AddressStreet { get; init; }
    public string AddressNumber { get; init; }
    public string AddressNeighborhood { get; init; }
    public string AddressCity { get; init; }
    public string AddressState { get; init; }
    public string AddressCountry { get; init; }
    public string AddressZipCode { get; init; }
    public IEnumerable<string> Hobbies { get; init; } = [];
    public IEnumerable<string> DesiredWorkplaceTypes { get; init; } = [];
    public IEnumerable<string> DesiredJobTypes { get; init; } = [];
}
