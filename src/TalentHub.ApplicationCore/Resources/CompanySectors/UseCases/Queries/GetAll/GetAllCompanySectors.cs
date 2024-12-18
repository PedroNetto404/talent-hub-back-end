using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Queries.GetAll;

public sealed record GetAllCompanySectorsQuery(
    int Limit,
    int Offset,
    string? SortBy,
    bool Ascending
) : ICachedQuery<PageResponse>
{
    public TimeSpan? Duration => TimeSpan.FromHours(12);

    public string Key => $"company_sectors_{Limit}_{Offset}_{SortBy}_{Ascending}";
}
