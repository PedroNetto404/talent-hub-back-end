using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Courses.UseCases.Commands.Delete;

public sealed record DeleteCourseCommand(
    Guid CourseId
) : ICommand;
