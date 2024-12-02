using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Courses.Dtos;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Courses.UseCases.Queries.GetAll;

public sealed class GetAllCoursesQueryHandler(
    IRepository<Course> repository
) : IQueryHandler<GetAllCoursesQuery, PagedResponse>
{
    public async Task<Result<PagedResponse>> Handle(
        GetAllCoursesQuery request,
        CancellationToken cancellationToken)
    {
        void containsSpec(ISpecificationBuilder<Course> query) => 
            query.Where(p => request.Ids.Contains(p.Id));

        List<Course> courses = await repository.GetPageAsync(
            request.Limit,
            request.Offset,
            request.SortBy,
            request.Ascending,
            additionalSpec: containsSpec,
            cancellationToken);
        int count = await repository.CountAsync(containsSpec, cancellationToken);

        CourseDto[] dtos = [
            .. courses.Select(CourseDto.FromEntity)
        ];

        return new PagedResponse(
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
