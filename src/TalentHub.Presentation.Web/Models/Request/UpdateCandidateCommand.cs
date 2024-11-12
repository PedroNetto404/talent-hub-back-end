using System.ComponentModel.DataAnnotations;
using MediatR;
using TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidate;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record UpdateCandidateRequest(
    [property: Required][property: StringLength(100, MinimumLength = 4)]
    string Name,

    [property: Required][property: StringLength(11, MinimumLength = 11)]
    string Phone,

    [property: Required]
    Address Address,

    [property: Required]
    WorkplaceType[] DesiredWorkplaceTypes,

    [property: Required]
    JobType[] DesiredJobTypes,

    [property: Range(1, double.MaxValue)]
    decimal? ExpectedRemuneration,

    [property: Url]
    string? InstagramUrl,

    [property: Url]
    string? LinkedinUrl,

    [property: Url]
    string? GithubUrl,

    [FileExtensions(Extensions = "pdf")]
    IFormFile? ResumeFile,

    [property: StringLength(500, MinimumLength = 10)]
    string? Summary,

    [property: Required]
    string[] Hobbies
);