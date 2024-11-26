using System.ComponentModel.DataAnnotations;

namespace TalentHub.Presentation.Web.Models.Request;

public abstract record UpdateExperienceRequest
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
    public required bool IsCurrent { get; init; }

    public IEnumerable<string> Activities { get; init; } = [];

    [Required]
    [AllowedValues(
       "incomplete",
       "in_progress",
       "completed"
   )]
    public string? Status { get; init; }


    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string? Description { get; init; }
}