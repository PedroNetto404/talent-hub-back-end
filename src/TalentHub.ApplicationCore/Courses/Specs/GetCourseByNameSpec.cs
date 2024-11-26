using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Courses.Specs;

public sealed class GetCourseByNameSpec : Specification<Course>
{
    public GetCourseByNameSpec(string name) => Query.AsNoTracking().Where(c => c.Name == name);
}