using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.CompanySectors.Dtos;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Queries.GetAll;

public sealed record GetAllCompanySectorsQuery(
    int Limit,
    int Offset,
    string? SortBy,
    SortOrder SortOrder
) : CachedQuery<PageResponse<CompanySectorDto>>
{
    public override TimeSpan Duration => TimeSpan.FromHours(12);
}
