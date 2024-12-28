using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.Certificates.Delete;

public sealed record DeleteCertificateRequest
{
    [FromRoute(Name = "candidateId")]
    public Guid CandidateId { get; init; }

    [FromRoute(Name = "certificateId")]
    public Guid CertificateId { get; init; }
}
