using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Courses.Dtos;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.ApplicationCore.Resources.Courses.UseCases.Queries.GetAll;

public sealed record GetAllCoursesQuery(
    string? NameLike,
    IEnumerable<Guid> RelatedSkillIds,
    int Limit,
    int Offset,
    string? SortBy,
    SortOrder SortOrder
) : CachedQuery<PageResponse<CourseDto>>
{
    public override TimeSpan Duration => TimeSpan.FromDays(1);
}
