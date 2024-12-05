using System.ComponentModel;
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

    [Range(1, 12)]
    public int? ExpectedGraduationStartMonth { get; init; }

    [Range(1900, 2100)]
    public int? ExpectedGraduationStartYear { get; init; }

    [Required]
    public bool IsCurrent { get; init; }

    public IEnumerable<string> Activities { get; init; } = [];

    [AllowedValues(
        "elementary_school",
        "high_school",
        "technical_education",
        "higher_degree",
        null
    )]
    [DefaultValue(null)]
    public string? Level { get; init; } = string.Empty;

    [AllowedValues(
        "incomplete",
        "in_progress",
        "completed"
    )]
    [DefaultValue(null)]
    public string? Status { get; init; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int? CurrentSemester { get; init; }

    public IEnumerable<string> AcademicEntities { get; init; } = [];

    public Guid? CourseId { get; init; }

    public Guid? UniversityId { get; init; }

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
        "other",
        null
    )]
    [DefaultValue(null)]
    public string? ProfessionalLevel { get; init; }
}
