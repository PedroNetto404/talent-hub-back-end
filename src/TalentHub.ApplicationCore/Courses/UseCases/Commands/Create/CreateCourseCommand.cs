using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Courses.Dtos;

namespace TalentHub.ApplicationCore.Courses.UseCases.Commands;

public sealed record CreateCourseCommand(
    string Name,
    string Description,
    IEnumerable<string> Tags,
    IEnumerable<Guid> RelatedSkills
) : ICommand<CourseDto>;
