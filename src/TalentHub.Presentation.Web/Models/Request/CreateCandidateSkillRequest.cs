using System.ComponentModel.DataAnnotations;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record CreateCandidateSkillRequest
{
    public Guid SkillId { get; init; }

    [AllowedValues(
        "beginner",
        "intermediate",
        "advanced"
    )]
    public string Proficiency { get; init; }
}
