using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateCandidateSkillProficiency;

public sealed record UpdateCandidateSkillProficiencyCommand(
    Guid CandidateId,
    Guid CandidateSkillId,
    string Proficiency
) : ICommand;
