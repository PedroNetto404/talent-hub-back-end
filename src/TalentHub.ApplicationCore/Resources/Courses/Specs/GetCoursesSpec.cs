using Ardalis.Specification;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Resources.Courses.Specs;

public sealed class GetCoursesSpec : GetPageSpec<Course>
{
    public GetCoursesSpec(
        IEnumerable<Guid> ids,
        int limit = int.MaxValue,
        int offset = 0,
        string? sortBy = null,
        bool ascending = true
    ) : base(limit, offset, sortBy, ascending)
    {
        if (ids.Any())
        {
            Query.Where(c => ids.Contains(c.Id));
        }
    }
}
