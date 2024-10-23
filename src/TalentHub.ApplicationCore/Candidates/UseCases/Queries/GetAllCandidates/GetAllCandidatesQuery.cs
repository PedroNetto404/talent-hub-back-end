using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Queries.GetAllCandidates;

public sealed record GetAllCandidatesQuery(
    int Limit,
    int Offset,
    string? SortBy,
    bool? Ascending
) : ICachedQuery<IEnumerable<CandidateDto>>
{
    public TimeSpan? Duration => TimeSpan.FromMinutes(5);
    public string Key => nameof(GetAllCandidatesQuery);
}