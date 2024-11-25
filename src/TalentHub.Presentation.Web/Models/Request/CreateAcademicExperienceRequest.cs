using System.ComponentModel.DataAnnotations;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record CreateAcademicExperienceRequest
{
    [Required]
    [RegularExpression("")]
    public string StartMonth { get; init; } = string.Empty;

    [Required]
    public string StartYear { get; init; } = string.Empty;

    public string? EndMonth { get; init; }

    public string? EndYear { get; init; }

    [Required]
    public bool IsCurrent { get; init; }

    [Required]
    [AllowedValues(
        "elementary_school",
        "high_school",
        "technical_education",
        "higher_degree"
    )]
    public string Level { get; init; } = string.Empty;

    [Required]
    [AllowedValues(
        "incomplete",
        "inprogress",
        "completed"
    )]
    public string Status { get; init; } = string.Empty;

    [Required]
    public Guid CourseId { get; init; }

    [Required]
    public Guid InstitutionId { get; init; }
}