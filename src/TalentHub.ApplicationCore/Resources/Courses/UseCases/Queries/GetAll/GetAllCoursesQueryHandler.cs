using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Courses.Dtos;
using TalentHub.ApplicationCore.Resources.Courses.Specs;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Courses.UseCases.Queries.GetAll;

public sealed class GetAllCoursesQueryHandler(
    IRepository<Course> repository
) : IQueryHandler<GetAllCoursesQuery, PageResponse<CourseDto>>
{
    public async Task<Result<PageResponse<CourseDto>>> Handle(
        GetAllCoursesQuery request,
        CancellationToken cancellationToken)
    {
        List<Course> courses = await repository.ListAsync(
            new GetCoursesSpec(
                request.NameLike,
                request.RelatedSkillIds,
                request.Limit,
                request.Offset,
                request.SortBy!,
                request.SortOrder
            ),
            cancellationToken);
        int count = await repository.CountAsync(new GetCoursesSpec(
            request.NameLike,
             request.RelatedSkillIds
            ),
             cancellationToken
            );

        CourseDto[] dtos = [
            .. courses.Select(CourseDto.FromEntity)
        ];

        return new PageResponse<CourseDto>(
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
