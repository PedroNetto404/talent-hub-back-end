using TalentHub.ApplicationCore.Resources.Candidates.Enums;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record UpdateCandidateSkillRequest
{
    public Proficiency? Proficiency { get; init; }
    public Dictionary<LanguageSkillType, Proficiency> SpecialProficiences { get; init; } = [];
}
