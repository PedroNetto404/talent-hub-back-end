using Ardalis.Specification;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Courses.Specs;

public sealed class GetAllCoursesSpec : PagedSpec<Course>
{
    public GetAllCoursesSpec(
        IEnumerable<Guid> ids,
        int limit,
        int offset,
        string? sortBy,
        bool ascending = true) : base(limit, offset, sortBy, ascending)
    {
        Query.Where(course => ids.Contains(course.Id))
             .AsNoTracking();
    }
}