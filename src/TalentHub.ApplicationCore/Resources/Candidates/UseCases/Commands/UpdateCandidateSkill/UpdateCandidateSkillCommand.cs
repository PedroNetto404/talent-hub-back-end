using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateCandidateSkill;

public sealed record UpdateCandidateSkillCommand(
    Guid CandidateId,
    Guid CandidateSkillId,
    Proficiency? Proficiency
) : ICommand;
