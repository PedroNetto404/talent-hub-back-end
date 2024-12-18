using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Courses.Dtos;
using TalentHub.ApplicationCore.Resources.Courses.Specs;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Courses.UseCases.Queries.GetAll;

public sealed class GetAllCoursesQueryHandler(
    IRepository<Course> repository
) : IQueryHandler<GetAllCoursesQuery, PageResponse>
{
    public async Task<Result<PageResponse>> Handle(
        GetAllCoursesQuery request,
        CancellationToken cancellationToken)
    {
        List<Course> courses = await repository.ListAsync(
            new GetCoursesSpec(
                request.Ids,
                request.Limit,
                request.Offset,
                request.SortBy!,
                request.Ascending
            ),
            cancellationToken);
        int count = await repository.CountAsync(new GetCoursesSpec(request.Ids), cancellationToken);

        CourseDto[] dtos = [
            .. courses.Select(CourseDto.FromEntity)
        ];

        return new PageResponse(
            new(
                dtos.Length,
                count,
                request.Offset,
                request.Limit
            ),
            dtos
        );
    }
}
