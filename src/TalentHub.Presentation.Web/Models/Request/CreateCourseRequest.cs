using System.ComponentModel.DataAnnotations;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record CreateCourseRequest
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; init; }

    public IEnumerable<string> Tags { get; init; } = [];

    public IEnumerable<Guid> RelatedSkills { get; init; } = [];
}