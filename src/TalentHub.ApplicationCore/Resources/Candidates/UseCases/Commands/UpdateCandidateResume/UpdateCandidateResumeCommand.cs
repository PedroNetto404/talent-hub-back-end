using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateCandidateResume;

public sealed record UpdateCandidateResumeCommand(
    Guid CandidateId,
    Stream File) : ICommand<CandidateDto>;
