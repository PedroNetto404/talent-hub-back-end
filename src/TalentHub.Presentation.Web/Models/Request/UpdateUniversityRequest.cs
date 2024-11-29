using System.ComponentModel.DataAnnotations;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record UpdateUniversityRequest
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; init; }
    
    [Url]
    public string? SiteUrl { get; init; }
}
