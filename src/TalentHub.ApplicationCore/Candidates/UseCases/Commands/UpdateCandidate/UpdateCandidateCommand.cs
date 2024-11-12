using TalentHub.ApplicationCore.Shared.ValueObjects;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidate;

public sealed record UpdateCandidateCommand(
    Guid CandidateId,
    string Name,
    string Phone,
    Address Address,
    WorkplaceType[] DesiredWorkplaceTypes,
    JobType[] DesiredJobTypes,
    decimal? ExpectedRemuneration,
    string? InstagramUrl,
    string? LinkedinUrl,
    string? GithubUrl,
    string? Summary,
    Stream? ResumeFile,
    string[] Hobbies
) : ICommand<CandidateDto>;
