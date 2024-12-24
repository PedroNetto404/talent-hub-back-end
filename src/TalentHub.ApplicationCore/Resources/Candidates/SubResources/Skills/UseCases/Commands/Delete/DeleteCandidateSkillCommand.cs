using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills.UseCases.Commands.Delete;

public sealed record DeleteCandidateSkillCommand(
    Guid CandidateId,
    Guid CandidateSkillId
) : ICommand<CandidateDto>;
