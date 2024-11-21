using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Candidates.Entities;

public class CandidateSkill : Entity
{
    protected CandidateSkill(Guid skillId, Proficiency proficiency)
    {
        SkillId = skillId;
        Proficiency = proficiency;
    }

    public static Result<CandidateSkill> Create(Guid skillId, Proficiency proficiency)
    {
        if(Guid.Empty == skillId) return new Error("candidate_skill", "candidate skill identifier is empty");
        return new CandidateSkill(skillId, proficiency);
    }
    
    protected CandidateSkill() { }
    
    public Guid SkillId { get; private set; }
    public Proficiency Proficiency { get; private set; }

    public void UpdateProficiency(Proficiency proficiency) =>
        Proficiency = proficiency;
}
