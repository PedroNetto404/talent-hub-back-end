using Ardalis.Specification;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Courses.Specs;

public sealed class GetAllCoursesSpec : PagedSpec<Course>
{
    public GetAllCoursesSpec(
        int limit,
        int offset,
        string? sortBy = null,
        bool ascending = true,
        params Guid[] ids) : base(limit, offset, sortBy, ascending)
    {
        if (ids.Length > 0) Query.Where(course => ids.Contains(course.Id));
        Query.AsNoTracking();
    }
}