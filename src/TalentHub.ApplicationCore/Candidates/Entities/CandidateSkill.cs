using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Candidates.Entities;

public class CandidateSkill(Guid skillId, Proficiency proficiency) : Entity
{
    public Guid SkillId { get; private set; } = skillId;
    public Proficiency Proficiency { get; private set; } = proficiency;

    public void UpdateProficiency(Proficiency proficiency) =>
        Proficiency = proficiency;
}
