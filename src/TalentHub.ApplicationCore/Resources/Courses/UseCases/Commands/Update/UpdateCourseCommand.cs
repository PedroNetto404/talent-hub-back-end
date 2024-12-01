using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Courses.UseCases.Commands.Update;

public sealed record UpdateCourseCommand(
    Guid Id,
    string Name,
    IEnumerable<string> Tags,
    IEnumerable<Guid> RelatedSkills
) : ICommand;
