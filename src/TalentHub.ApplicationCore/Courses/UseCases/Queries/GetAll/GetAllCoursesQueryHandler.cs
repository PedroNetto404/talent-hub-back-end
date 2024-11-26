using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Courses.Dtos;
using TalentHub.ApplicationCore.Courses.Specs;

namespace TalentHub.ApplicationCore.Courses.UseCases.Queries.GetAll;

public sealed class GetAllCoursesQueryHandler(
    IRepository<Course> repository
) : IQueryHandler<GetAllCoursesQuery, IEnumerable<CourseDto>>
{
    public async Task<Result<IEnumerable<CourseDto>>> Handle(
        GetAllCoursesQuery request,
        CancellationToken cancellationToken)
    {
        var courses = await repository.ListAsync(new GetAllCoursesSpec(
            request.Ids,
            request.Limit,
            request.Offset,
            request.SortBy,
            request.Ascending
        ), cancellationToken);

        return Result.Ok(courses.Select(CourseDto.FromEntity));
    }
}
