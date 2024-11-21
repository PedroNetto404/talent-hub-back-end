using MediatR;
using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidateResume;

public sealed record UpdateCandidateResumeCommand(
    Guid CandidateId,
    Stream File) : ICommand<CandidateDto>;