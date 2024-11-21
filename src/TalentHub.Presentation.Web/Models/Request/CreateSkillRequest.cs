using System.ComponentModel.DataAnnotations;

namespace TalentHub.Presentation.Web.Models.Request;

public record CreateSkillRequest
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; init; }

    [Required]
    [AllowedValues("language", "hard", "soft")]
    public required string Type { get; init; }

    public IEnumerable<string> Tags { get; init; } = [];
}
