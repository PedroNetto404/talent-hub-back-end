using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidateSkill;

public sealed record UpdateCandidateSkillCommand(
    Guid CandidateId,
    Guid CandidateSkillId,
    Proficiency? Proficiency
) : ICommand;
