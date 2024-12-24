using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.ApplicationCore.Resources.Universities.UseCases.Queries.GetAll;

public sealed record GetAllUniversitiesQuery(
    IEnumerable<Guid> Ids,
    int Limit,
    int Offset,
    string? SortBy = null,
    SortOrder SortOrder = SortOrder.Ascending
    ) : CachedQuery<PageResponse>
{
    public override TimeSpan Duration => TimeSpan.FromHours(12);
}
