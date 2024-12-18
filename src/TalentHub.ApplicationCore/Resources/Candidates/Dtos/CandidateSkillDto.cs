using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills;

namespace TalentHub.ApplicationCore.Resources.Candidates.Dtos;

public sealed record CandidateSkillDto(
    Guid Id,
    Guid SkillId,
    Proficiency Proficiency
)
{
    public static CandidateSkillDto FromEntity(CandidateSkill candidateSkill) =>
        new(
            candidateSkill.Id,
            candidateSkill.SkillId,
            candidateSkill.Proficiency
        );
}

