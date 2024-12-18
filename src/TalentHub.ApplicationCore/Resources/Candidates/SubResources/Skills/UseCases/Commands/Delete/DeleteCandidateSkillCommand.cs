using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills.UseCases.Commands.Delete;

public sealed record DeleteCandidateSkillCommand(
    Guid CandidateId,
    Guid CandidateSkillId
) : ICommand;
