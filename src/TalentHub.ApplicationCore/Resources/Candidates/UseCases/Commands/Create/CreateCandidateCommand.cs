using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Create;

public sealed record CreateCandidateCommand(
    string Name,
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
