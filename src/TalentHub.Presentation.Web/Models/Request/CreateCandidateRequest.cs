using System.ComponentModel.DataAnnotations;
using TalentHub.ApplicationCore.Candidates.UseCases.Commands.CreateCandidate;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed class CreateCandidateRequest
{
    [Required]
    [StringLength(100, MinimumLength = 4)]
    public required string Name { get; init; }

    [Required]
    [EmailAddress]
    public required string Email { get; init; }

    [Required]
    [StringLength(11, MinimumLength = 11)]
    public required string Phone { get; init; }

    [Required]
    public required DateTime BirthDate { get; init; }

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

    public string? Summary { get; init; }

    public IEnumerable<string> Hobbies { get; init; } = [];

    public CreateCandidateCommand ToCommand() =>
        new(
            Name,
            Email,
            Phone,
            DateOnly.FromDateTime(BirthDate),
            Address,
            DesiredJobTypes.ToArray(),
            DesiredWorkplaceTypes.ToArray(),
            Summary,
            GithubUrl,
            InstagramUrl,
            LinkedinUrl,
            ExpectedRemuneration,
            Hobbies.ToArray()
        );
}
