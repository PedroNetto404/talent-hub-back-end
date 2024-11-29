using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;

namespace TalentHub.ApplicationCore.Candidates.Dtos;

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

