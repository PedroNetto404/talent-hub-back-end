using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Courses.UseCases.Commands.Update;

public sealed record UpdateCourseCommand(
    Guid Id,
    string Name,
    string Description,
    IEnumerable<string> Tags,
    IEnumerable<Guid> RelatedSkills
) : ICommand;
