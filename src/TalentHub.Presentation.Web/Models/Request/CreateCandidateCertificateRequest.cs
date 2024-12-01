using System.ComponentModel.DataAnnotations;

namespace TalentHub.Presentation.Web.Models.Request;

public record CreateCandidateCertificateRequest
{
    [Required]
    public required string Name { get; init; }
    
    [Required]
    public required string Issuer { get; init; }
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public double Workload { get; init; }
    
    [Url]
    public string? Url { get; init; }

    public IEnumerable<Guid> RelatedSkills { get; init; } = [];
}
