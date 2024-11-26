using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Courses.Dtos;

namespace TalentHub.ApplicationCore.Courses.UseCases.Queries.GetAll;

public sealed record GetAllCoursesQuery(
    IEnumerable<Guid> Ids,
    int Limit,
    int Offset,
    string? SortBy,
    bool Ascending = true
) : ICachedQuery<IEnumerable<CourseDto>>
{
    public TimeSpan? Duration => TimeSpan.FromDays(1);

    public string Key => nameof(GetAllCoursesQuery);
}
