using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Courses.Dtos;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Courses.UseCases.Queries.GetAll;

public sealed record GetAllCoursesQuery(
    IEnumerable<Guid> Ids,
    int Limit,
    int Offset,
    string? SortBy,
    bool Ascending = true
) : ICachedQuery<PagedResponse>
{
    public TimeSpan? Duration => TimeSpan.FromDays(1);

    public string Key => nameof(GetAllCoursesQuery);
}
