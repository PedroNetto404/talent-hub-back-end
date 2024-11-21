using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Skills;
using TalentHub.ApplicationCore.Skills.Enums;

namespace TalentHub.ApplicationCore.Candidates.Dtos;

public sealed record CandidateSkillDto(
    Guid Id,
    Guid SkillId,
    string SkillName,
    SkillType SkillType,
    Proficiency Proficiency,
    IReadOnlyDictionary<LanguageSkillType, Proficiency>? LanguageSpecialProficiences
)
{
    public static CandidateSkillDto FromEntity(CandidateSkill candidateSkill, Skill skill) =>
        new(
            candidateSkill.Id,
            skill.Id,
            skill.Name,
            skill.Type,
            candidateSkill.Proficiency,
            (candidateSkill as LanguageProficiency)?.SpecialProficiences
        );
}

