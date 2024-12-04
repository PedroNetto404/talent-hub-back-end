using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Queries.GetAll;

public sealed record GetAllCompaniesQuery(
    string? NameLike,
    bool? HasJobOpening,
    IEnumerable<Guid> SectorIds,
    string? LocationLike,
    int Limit,
    int Offset,
    string? SortBy,
    bool Ascending
) : IQuery<PagedResponse>;