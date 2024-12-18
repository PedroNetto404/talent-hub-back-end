using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Resources.Courses.Specs;

public sealed class GetCourseByNameSpec : Specification<Course>
{
    public GetCourseByNameSpec(string name)
    {
        Query.Where(c => c.Name == name).AsNoTracking();
    }
}
