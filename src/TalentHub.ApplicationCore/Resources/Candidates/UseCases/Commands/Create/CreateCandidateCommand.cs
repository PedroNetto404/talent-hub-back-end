using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Create;

public sealed record CreateCandidateCommand(
    string Name,
    bool AutoMatchEnabled,
    string Phone,
    DateOnly BirthDate,
    string AddressStreet,
    string AddressNumber,
    string AddressNeighborhood,
    string AddressCity,
    string AddressState,
    string AddressCountry,
    string AddressZipCode,
    IEnumerable<string> DesiredJobTypes,
    IEnumerable<string> DesiredWorkplaceTypes,
    string? Summary,
    string? GithubUrl,
    string? InstagramUrl,
    string? LinkedinUrl,
    decimal? ExpectedRemuneration,
    IEnumerable<string> Hobbies
) : ICommand<CandidateDto>;
