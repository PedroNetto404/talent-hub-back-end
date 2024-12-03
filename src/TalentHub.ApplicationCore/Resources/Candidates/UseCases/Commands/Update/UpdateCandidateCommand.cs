using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Update;

public sealed record UpdateCandidateCommand(
    Guid CandidateId,
    string Name,
    bool AutoMatchEnabled,
    string Phone,
    Address Address,
    IEnumerable<string> DesiredWorkplaceTypes,
    IEnumerable<string> DesiredJobTypes,
    decimal? ExpectedRemuneration,
    string? InstagramUrl,
    string? LinkedinUrl,
    string? GithubUrl,
    string? Summary,
    IEnumerable<string> Hobbies
) : ICommand;
