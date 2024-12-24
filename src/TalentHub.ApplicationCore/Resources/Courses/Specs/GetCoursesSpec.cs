using Ardalis.Specification;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Resources.Courses.Specs;

public sealed class GetCoursesSpec : GetPageSpec<Course>
{
    public GetCoursesSpec(
        IEnumerable<Guid> ids,
        int limit = int.MaxValue,
        int offset = 0,
        string? sortBy = null,
        SortOrder sortOrder = SortOrder.Ascending
    ) : base(limit, offset, sortBy, sortOrder)
    {
        if (ids.Any())
        {
            Query.Where(c => ids.Contains(c.Id));
        }
    }
}
