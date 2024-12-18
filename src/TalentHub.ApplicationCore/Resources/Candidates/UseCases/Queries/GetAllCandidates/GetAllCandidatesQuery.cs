using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetAllCandidates;

public sealed record GetAllCandidatesQuery(
    IEnumerable<Guid> SkillIds,
    IEnumerable<string> Languages,
    int Limit,
    int Offset,
    string? SortBy,
    bool Ascending = true
) : IQuery<PageResponse>;
