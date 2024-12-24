using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Certificates.Create;

public sealed record CreateCertificateRequest(
    [property: FromRoute(Name = "CandidateId")] Guid CandidateId,
    string Name,
    string Issuer,
    double Workload,
    IEnumerable<Guid> RelatedSkills
);
