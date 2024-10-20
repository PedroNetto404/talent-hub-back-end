using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Skills;

namespace TalentHub.ApplicationCore.Candidates.Entities;

public sealed class CandidateLanguagueSkill(Skill skill, Proficiency proficiency) : CandidateSkill(skill, proficiency)
{
    private readonly Dictionary<LanguageSkillType, Proficiency> _specialProficiency = new()
    {
        [LanguageSkillType.Writing] = Proficiency.Beginner,
        [LanguageSkillType.Listening] = Proficiency.Beginner,
        [LanguageSkillType.Speaking] = Proficiency.Beginner
    };

    public void UpdateSpecialProficiency(LanguageSkillType type, Proficiency proficiency) =>
        _specialProficiency[type] = proficiency;
}