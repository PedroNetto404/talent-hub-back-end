using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands.Create;

public record CreateCandidateCertificateCommand(
    Guid CandidateId,
    string Name,
    string Issuer,
    double Workload,
    IEnumerable<Guid> RelatedSkills
) : ICommand<CandidateDto>;
