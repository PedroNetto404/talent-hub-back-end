using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Courses.Dtos;

namespace TalentHub.ApplicationCore.Courses.UseCases.Queries.GetById;

public sealed record GetCourseByIdQuery(Guid CourseId) : IQuery<CourseDto>;

public sealed class GetCourseByIdQueryHandler(
    IRepository<Course> courseRepository) : IQueryHandler<GetCourseByIdQuery, CourseDto>
{
    public async Task<Result<CourseDto>> Handle(
        GetCourseByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetByIdAsync(
            request.CourseId, 
            cancellationToken);
        if (course is null) return NotFoundError.Value;

        return CourseDto.FromEntity(course);
    }
}
