using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills.UseCases.Commands.Update;

public sealed record UpdateCandidateSkillProficiencyCommand(
    Guid CandidateId,
    Guid CandidateSkillId,
    string Proficiency
) : ICommand;
