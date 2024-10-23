using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Skills.Enums;

namespace TalentHub.ApplicationCore.Candidates.Dtos;

public sealed record CandidateSkillDto(
    Guid SkillId,
    string SkillName,
    SkillType SkillType,
    Proficiency Proficiency,
    IReadOnlyDictionary<LanguageSkillType, Proficiency>? LanguageSpecialProficiences
)
{
    public static CandidateSkillDto FromEntity(CandidateSkill skill) =>
        new(
            skill.SkillId,
            skill.SkillName,
            skill.SkillType,
            skill.Proficiency,
            (skill as CandidateLanguageSkill)?.SpecialProficiences
        );
}

