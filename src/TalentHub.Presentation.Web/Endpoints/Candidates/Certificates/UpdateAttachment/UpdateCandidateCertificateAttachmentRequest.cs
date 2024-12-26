using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Certificates.UpdateAttachment;

public sealed record UpdateCandidateCertificateAttachmentRequest(
    [property: FromRoute(Name = "candidateId")] Guid CandidateId,
    [property: FromRoute(Name = "certificateId")] Guid CertificateId,
    IFormFile File
);
