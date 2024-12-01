using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.DeleteCandidateSkill;

public sealed record DeleteCandidateSkillCommand(
    Guid CandidateId,
    Guid CandidateSkillId
) : ICommand;
