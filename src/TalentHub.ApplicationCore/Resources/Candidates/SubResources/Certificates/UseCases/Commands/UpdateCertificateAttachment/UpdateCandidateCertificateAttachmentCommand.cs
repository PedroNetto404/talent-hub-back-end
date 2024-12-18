using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands.UpdateCertificateAttachment;

public sealed record UpdateCandidateCertificateAttachmentCommand(
    Guid CandidateId,
    Guid CertificateId,
    Stream AttachmentFile,
    string ContentType) : ICommand<CandidateDto>;
