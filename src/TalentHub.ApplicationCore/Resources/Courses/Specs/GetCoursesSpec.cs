using Ardalis.Specification;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Resources.Courses.Specs;

public sealed class GetCoursesSpec : GetPageSpec<Course>
{
    public GetCoursesSpec(
        string? nameLike,
        IEnumerable<Guid> relatedSkillIds,
        int limit = int.MaxValue,
        int offset = 0,
        string? sortBy = null,
        SortOrder sortOrder = SortOrder.Ascending
    ) : base(limit, offset, sortBy, sortOrder)
    {
        if (!string.IsNullOrWhiteSpace(nameLike))
        {
            Query.Where(course => course.Name.Contains(nameLike));
        }

        if (relatedSkillIds.Any())
        {
            Query.Where(course => course.RelatedSkills.Any(relatedSkillIds.Contains));
        }
    }
}
