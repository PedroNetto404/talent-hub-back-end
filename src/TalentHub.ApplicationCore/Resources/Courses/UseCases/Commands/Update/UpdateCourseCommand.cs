using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Courses.Dtos;

namespace TalentHub.ApplicationCore.Resources.Courses.UseCases.Commands.Update;

public sealed record UpdateCourseCommand(
    Guid Id,
    string Name,
    IEnumerable<string> Tags,
    IEnumerable<Guid> RelatedSkills
) : ICommand<CourseDto>;
