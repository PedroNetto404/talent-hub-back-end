using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Courses.UseCases.Commands.Create.Delete;

public sealed class DeleteCourseCommandHandler(IRepository<Course> courseRepository) : ICommandHandler<DeleteCourseCommand>
{
    public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
        if(course is null) return NotFoundError.Value;

        await courseRepository.DeleteAsync(course, cancellationToken);
        return Result.Ok();
    }
}