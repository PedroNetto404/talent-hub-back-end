using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Skills;
using TalentHub.ApplicationCore.Skills.Enums;

namespace TalentHub.ApplicationCore.Candidates.Entities;

public class CandidateSkill : Entity
{
    public CandidateSkill(Skill skill, Proficiency proficiency)
    {
        if (skill.IsSuggestion)
        {
            throw new InvalidOperationException("skills that have been suggested can only be used after approval.");
        }

        SkillId = skill.Id;
        SkillName = skill.Name;
        SkillType = skill.Type;
        Proficiency = proficiency;
    }

    public Guid SkillId { get; private set; }
    public string SkillName { get; private set; }
    public SkillType SkillType { get; private set; }
    public Proficiency Proficiency { get; private set; }

    public void UpdateProficiency(Proficiency proficiency) =>
        Proficiency = proficiency;
}
