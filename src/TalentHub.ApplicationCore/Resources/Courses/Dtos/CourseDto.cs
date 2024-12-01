namespace TalentHub.ApplicationCore.Resources.Courses.Dtos;

public sealed record CourseDto(
    Guid Id,
    string Name,
    IEnumerable<string> Tags,
    IEnumerable<Guid> RelatedSkills
)
{
    public static CourseDto FromEntity(Course course) =>
        new(
            course.Id,
            course.Name,
            [.. course.Tags],
            [.. course.RelatedSkills]
        );
}
