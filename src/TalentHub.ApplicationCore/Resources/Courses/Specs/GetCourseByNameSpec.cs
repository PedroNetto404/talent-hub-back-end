using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Courses.Specs;

public sealed class GetCourseByNameSpec : Specification<Course>
{
    public GetCourseByNameSpec(string name) => 
        Query
            .Where(c => c.Name == name)
            .AsNoTracking();

    public GetCourseByNameSpec(params string[] names) => 
        Query
            .Where(c => names.Contains(c.Name))
            .AsNoTracking();
}