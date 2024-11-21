using System.ComponentModel.DataAnnotations;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record UpdateCandidateRequest
{
    [Required]
    [StringLength(100, MinimumLength = 4)]
    public required string Name { get; init; }

    [Required]
    [StringLength(11, MinimumLength = 11)]
    public required string Phone { get; init; }

    [Required]
    public required Address Address { get; init; }

    public IEnumerable<WorkplaceType> DesiredWorkplaceTypes { get; init; } = [];

    public IEnumerable<JobType> DesiredJobTypes { get; init; } = [];

    [Range(1, double.MaxValue)]
    public decimal? ExpectedRemuneration { get; init; }

    [Url]
    public string? InstagramUrl { get; init; }

    [Url]
    public string? LinkedinUrl { get; init; }

    [Url]
    public string? GithubUrl { get; init; }

    [StringLength(500, MinimumLength = 10)]
    public string? Summary { get; init; }

    public IEnumerable<string> Hobbies { get; init; } = [];
}
