using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Resources.Courses.UseCases.Commands.Create.Delete;

public sealed class DeleteCourseCommandHandler(IRepository<Course> courseRepository) : ICommandHandler<DeleteCourseCommand>
{
    public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        Course? course = await courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
        if (course is null)
        {
            return Error.NotFound("course");
        }

        await courseRepository.DeleteAsync(course, cancellationToken);
        return Result.Ok();
    }
}
