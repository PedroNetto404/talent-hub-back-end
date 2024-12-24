using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.ApplicationCore.Resources.Courses.UseCases.Queries.GetAll;

public sealed record GetAllCoursesQuery(
    IEnumerable<Guid> Ids,
    int Limit,
    int Offset,
    string? SortBy,
    SortOrder SortOrder
) : CachedQuery<PageResponse>
{
    public override TimeSpan Duration => TimeSpan.FromDays(1);
}
