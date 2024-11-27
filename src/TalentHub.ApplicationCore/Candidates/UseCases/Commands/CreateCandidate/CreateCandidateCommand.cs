using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.CreateCandidate;

public sealed record CreateCandidateCommand(
    string Name,
    string Email,
    string Phone,
    DateOnly BirthDate,
    Address Address,
    IEnumerable<string> DesiredJobTypes,
    IEnumerable<string> DesiredWorkplaceTypes,
    string? Summary,
    string? GithubUrl,
    string? InstagramUrl,
    string? LinkedinUrl,
    decimal? ExpectedRemuneration,
    IEnumerable<string> Hobbies
) : ICommand<CandidateDto>;