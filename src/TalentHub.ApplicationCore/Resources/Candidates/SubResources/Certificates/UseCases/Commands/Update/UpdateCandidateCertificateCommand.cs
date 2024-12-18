using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands.Update;

public sealed record UpdateCandidateCertificateCommand(
    Guid CandidateId,
    Guid CertificateId,
    string Name,
    string Issuer,
    double Workload,
    string? Url,
    IEnumerable<Guid> RelatedSkills
) : ICommand;