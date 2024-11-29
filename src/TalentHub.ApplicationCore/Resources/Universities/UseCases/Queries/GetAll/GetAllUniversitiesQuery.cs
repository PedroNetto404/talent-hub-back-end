using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Universities.Dtos;

namespace TalentHub.ApplicationCore.Universities.UseCases.Queries.GetAll;

public sealed record GetAllUniversitiesQuery(
    IEnumerable<Guid> Ids,
    int Limit,
    int Offset,
    string? SortBy = null,
    bool Ascending = true
    ) : ICachedQuery<PagedResponse<UniversityDto>>
{
    public TimeSpan? Duration => TimeSpan.FromHours(12);
    public string Key => nameof(GetAllUniversitiesQuery);
}