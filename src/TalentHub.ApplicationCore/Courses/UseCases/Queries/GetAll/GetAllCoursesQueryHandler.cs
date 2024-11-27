using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Courses.Dtos;
using TalentHub.ApplicationCore.Courses.Specs;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Courses.UseCases.Queries.GetAll;

public sealed class GetAllCoursesQueryHandler(
    IRepository<Course> repository
) : IQueryHandler<GetAllCoursesQuery, PagedResponse<CourseDto>>
{
    public async Task<Result<PagedResponse<CourseDto>>> Handle(
        GetAllCoursesQuery request,
        CancellationToken cancellationToken)
    {
        var courses = await repository.ListAsync(new GetAllCoursesSpec(
            request.Limit,
            request.Offset,
            request.SortBy,
            request.Ascending,
            request.Ids.ToArray()
        ), cancellationToken);

        var count = await repository.CountAsync(
            new GetAllCoursesSpec(
                int.MaxValue,
                0
            ), 
            cancellationToken
        );
        
        var dtos = courses.Select(CourseDto.FromEntity).ToArray();
        return new PagedResponse<CourseDto>(
            new(dtos.Length, count, request.Offset, request.Limit),
            dtos);
    }
}