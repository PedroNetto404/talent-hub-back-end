using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Courses.UseCases.Commands.Create.Delete;

public sealed record DeleteCourseCommand(
    Guid CourseId
) : ICommand;
