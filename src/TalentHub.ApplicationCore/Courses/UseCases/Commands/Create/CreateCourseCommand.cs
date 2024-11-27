using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Courses.Dtos;

namespace TalentHub.ApplicationCore.Courses.UseCases.Commands.Create;

public sealed record CreateCourseCommand(
    string Name,
    IEnumerable<string> Tags,
    IEnumerable<Guid> RelatedSkills
) : ICommand<CourseDto>;
