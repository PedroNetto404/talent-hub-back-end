using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.CreateCandidateCertificate;

public record CreateCandidateCertificateCommand(
    Guid CandidateId,
    string Name,
    string Issuer,
    double Workload,
    string? Url,
    IEnumerable<Guid> RelatedSkills
) : ICommand<CandidateDto>;
