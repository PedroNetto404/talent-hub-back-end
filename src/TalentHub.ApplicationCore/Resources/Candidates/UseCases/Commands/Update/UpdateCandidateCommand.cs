using TalentHub.ApplicationCore.Shared.ValueObjects;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.Update;

public sealed record UpdateCandidateCommand(
    Guid CandidateId,
    string Name,
    string Phone,
    Address Address,
    IEnumerable<WorkplaceType> DesiredWorkplaceTypes,
    IEnumerable<JobType> DesiredJobTypes,
    decimal? ExpectedRemuneration,
    string? InstagramUrl,
    string? LinkedinUrl,
    string? GithubUrl,
    string? Summary,
    IEnumerable<string> Hobbies
) : ICommand;
