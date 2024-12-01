using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Courses.Dtos;

namespace TalentHub.ApplicationCore.Resources.Courses.UseCases.Queries.GetById;

public sealed record GetCourseByIdQuery(Guid CourseId) : IQuery<CourseDto>;

public sealed class GetCourseByIdQueryHandler(
    IRepository<Course> courseRepository) : IQueryHandler<GetCourseByIdQuery, CourseDto>
{
    public async Task<Result<CourseDto>> Handle(
        GetCourseByIdQuery request, 
        CancellationToken cancellationToken)
    {
        request.Deconstruct(out Guid courseId);

        Course? course = await courseRepository.GetByIdAsync(courseId, cancellationToken);
        if (course is null)
        {
            return Error.NotFound("course");
        }

        return CourseDto.FromEntity(course);
    }
}
