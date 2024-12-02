using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.DeleteCandidateCertificate;

public sealed record DeleteCandidateCertificateCommand(
    Guid CandidateId,
    Guid CertificateId
) : ICommand;
