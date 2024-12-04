using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record CreateCandidateRequest
{
    [Required]
    [StringLength(100, MinimumLength = 4)]
    public required string Name { get; init; }

    [DefaultValue(true)]
    [Required] public required bool AutoMatchEnabled { get; init; }

    [Required]
    [StringLength(11, MinimumLength = 11)]
    public required string Phone { get; init; }

    [Required] public required DateTime BirthDate { get; init; }

    [Required] public required Address Address { get; init; }

    public IEnumerable<string> DesiredWorkplaceTypes { get; init; } = [];

    public IEnumerable<string> DesiredJobTypes { get; init; } = [];

    [Range(1, double.MaxValue)] public decimal? ExpectedRemuneration { get; init; }

    [Url] public string? InstagramUrl { get; init; }

    [Url] public string? LinkedinUrl { get; init; }

    [Url] public string? GithubUrl { get; init; }

    public string? Summary { get; init; }

    public IEnumerable<string> Hobbies { get; init; } = [];
}
