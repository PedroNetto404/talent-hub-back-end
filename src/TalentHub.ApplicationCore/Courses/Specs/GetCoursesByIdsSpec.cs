using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Courses.Specs;

public sealed class GetCoursesByIdsSpec : Specification<Course>
{
    public GetCoursesByIdsSpec(IEnumerable<Guid> ids) =>
        Query.Where(course => ids.Contains(course.Id))
             .AsNoTracking();
}