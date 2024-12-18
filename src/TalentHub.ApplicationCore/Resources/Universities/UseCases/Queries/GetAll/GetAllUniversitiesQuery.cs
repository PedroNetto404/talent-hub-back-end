using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Universities.UseCases.Queries.GetAll;

public sealed record GetAllUniversitiesQuery(
    IEnumerable<Guid> Ids,
    int Limit,
    int Offset,
    string? SortBy = null,
    bool Ascending = true
    ) : ICachedQuery<PageResponse>
{
    public TimeSpan? Duration => TimeSpan.FromHours(12);
    public string Key => nameof(GetAllUniversitiesQuery);
}
