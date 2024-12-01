using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Courses.Dtos;

namespace TalentHub.ApplicationCore.Resources.Courses.UseCases.Commands.Create;

public sealed record CreateCourseCommand(
    string Name,
    IEnumerable<string> Tags,
    IEnumerable<Guid> RelatedSkills
) : ICommand<CourseDto>;
