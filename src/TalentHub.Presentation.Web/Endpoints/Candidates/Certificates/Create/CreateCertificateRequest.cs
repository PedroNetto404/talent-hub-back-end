namespace TalentHub.Presentation.Web.Endpoints.Candidates.Certificates.Create;

public sealed record CreateCertificateRequest
{
    public string Name { get; init; }
    public string Issuer { get; init; }
    public double Workload { get; init; }
    public IEnumerable<Guid> RelatedSkills { get; init; }
}
