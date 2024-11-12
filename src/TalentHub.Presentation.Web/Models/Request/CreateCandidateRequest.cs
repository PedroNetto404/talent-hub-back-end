using System.ComponentModel.DataAnnotations;
using TalentHub.ApplicationCore.Candidates.UseCases.Commands.CreateCandidate;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;
using TalentHub.Presentation.Web.Attributes;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record CreateCandidateRequest(
    [property: Required][property: StringLength(100, MinimumLength = 4)]
    string Name,

    [property: Required][property: EmailAddress]
    string Email,

    [property: Required][property: StringLength(11, MinimumLength = 11)]
    string Phone,

    [property: Required][property: NotInFuture]
    DateOnly BirthDate,

    [property: Required][property: Address]
    Address Address,

    [property: EnumDataType(typeof(WorkplaceType))]
    WorkplaceType[] DesiredWorkplaceTypes,

    [property: EnumDataType(typeof(JobType))]
    JobType[] DesiredJobTypes,

    [property: Range(1, double.MaxValue)]
    decimal? ExpectedRemuneration,

    [property: Url]
    string? InstagramUrl,

    [property: Url]
    string? LinkedinUrl,

    [property: Url]
    string? GithubUrl,

    [property: FileExtensions(Extensions = "pdf")]
    IFormFile? ResumeFile,

    [property: StringLength(500, MinimumLength = 10)]
    string? Summary,

    string[] Hobbies
)
{
    public CreateCandidateCommand ToCommand() =>
        new(
            Name,
            Email,
            Phone,
            BirthDate,
            Address,
            DesiredJobTypes,
            DesiredWorkplaceTypes,
            Summary,
            GithubUrl,
            ResumeFile?.OpenReadStream(),
            InstagramUrl,
            LinkedinUrl,
            ExpectedRemuneration,
            Hobbies
        );
}
