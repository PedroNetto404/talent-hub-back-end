using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands.Delete;

public sealed record DeleteCandidateCertificateCommand(
    Guid CandidateId,
    Guid CertificateId
) : ICommand;
