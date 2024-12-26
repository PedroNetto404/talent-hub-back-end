using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Queries.GetAll;

public sealed record GetAllCompaniesQuery(
    string? NameLike,
    bool? HasJobOpening,
    IEnumerable<Guid> SectorIds,
    string? LocationLike,
    int Limit,
    int Offset,
    string? SortBy,
    SortOrder Ascending
) : IQuery<PageResponse<CompanyDto>>;
