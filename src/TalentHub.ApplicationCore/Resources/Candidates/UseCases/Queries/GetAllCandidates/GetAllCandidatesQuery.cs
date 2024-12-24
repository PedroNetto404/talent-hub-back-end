using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetAllCandidates;

public sealed record GetAllCandidatesQuery(
    IEnumerable<Guid> SkillIds,
    IEnumerable<string> Languages,
    int Limit,
    int Offset,
    string? SortBy,
    SortOrder SortOrder
) : CachedQuery<PageResponse>;
