using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateCandidateCertificate;

public sealed record UpdateCandidateCertificateCommand(
    Guid CandidateId,
    Guid CertificateId,
    string Name,
    string Issuer,
    double Workload,
    string? Url,
    IEnumerable<Guid> RelatedSkills
) : ICommand;