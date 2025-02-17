using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills;

public sealed class CandidateSkill : VersionedEntity
{
    private CandidateSkill(Guid skillId, Proficiency proficiency)
    {
        SkillId = skillId;
        Proficiency = proficiency;
    }

    public static Result<CandidateSkill> Create(Guid skillId, Proficiency proficiency)
    {
        if (Guid.Empty == skillId)
        {
            return Error.InvalidInput("candidate skill identifier is empty");
        }

        return new CandidateSkill(skillId, proficiency);
    }

#pragma warning disable CS0628 // New protected member declared in sealed type
    protected CandidateSkill() { }
#pragma warning restore CS0628 // New protected member declared in sealed type

    public Guid SkillId { get; private set; }
    public Proficiency Proficiency { get; private set; }

    public void UpdateProficiency(Proficiency proficiency) =>
        Proficiency = proficiency;
}
