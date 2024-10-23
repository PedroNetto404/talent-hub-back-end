using System.ComponentModel.DataAnnotations;
using TalentHub.ApplicationCore.Candidates.UseCases.Commands.CreateCandidate;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;
using TalentHub.Presentation.Web.Attributes;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record CreateCandidateRequest
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
    [NotInFuture]
    public required DateOnly BirthDate { get; init; }

    [Required]
    [Address]
    public required Address Address { get; init; }

    [Required]
    [EnumDataType(typeof(WorkplaceType))]
    public required WorkplaceType DesiredWorkplaceType { get; init; }

    [Required]
    [EnumDataType(typeof(JobType))]
    public required JobType DesiredJobType { get; init; }

    [Range(1, double.MaxValue)]
    public decimal? ExpectedRemuneration { get; init; }

    [Url]
    public string? InstagramUrl { get; init; }

    [Url]
    public string? LinkedinUrl { get; init; }

    [Url]
    public string? GithubUrl { get; init; }

    public IFormFile? ResumeFile { get; init; }

    [StringLength(10, MinimumLength = 500)]
    public string? Summary { get; init; }

    public string[] Hobbies { get; init; } = [];

    public CreateCandidateCommand ToCommand() =>
        new(
            Name,
            Email,
            Phone,
            BirthDate,
            Address,
            DesiredJobType,
            DesiredWorkplaceType,
            Summary,
            GithubUrl,
            ResumeFile?.OpenReadStream(),
            InstagramUrl,
            LinkedinUrl,
            ExpectedRemuneration,
            Hobbies
        );
}