using System.ComponentModel.DataAnnotations;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record CreateExperienceRequest
{

    [Required]
    [Range(1, 12)]
    public required int StartMonth { get; init; }

    [Required]
    [Range(1900, 2100)]
    public required int StartYear { get; init; }

    [Range(1, 12)]
    public int? EndMonth { get; init; }

    [Range(1900, 2100)]
    public int? EndYear { get; init; }

    [Required]
    public bool IsCurrent { get; init; }

    public IEnumerable<string> Activities { get; init; } = [];

    [AllowedValues(
        "elementary_school",
        "high_school",
        "technical_education",
        "higher_degree"
    )]
    public string? Level { get; init; } = string.Empty;

    [AllowedValues(
        "incomplete",
        "in_progress",
        "completed"
    )]
    public string? Status { get; init; } = string.Empty;

    public Guid? CourseId { get; init; }
 
    public Guid? InstitutionId { get; init; }

    public string? Position { get; init; }

    public string? Description { get; init; }

    public string? Company { get; init; }

    [AllowedValues(
        "intern",
        "trainee",
        "junior",
        "mid_level",
        "senior",
        "analyst",
        "other"
    )]
    public string? ProfessionalLevel { get; init; }
}