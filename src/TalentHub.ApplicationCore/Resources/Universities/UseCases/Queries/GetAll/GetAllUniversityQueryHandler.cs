using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Universities.Dtos;
using TalentHub.ApplicationCore.Resources.Universities.Specs;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Universities.UseCases.Queries.GetAll;

public sealed class GetAllUniversityQueryHandler(
    IRepository<University> universityRepository) : IQueryHandler<GetAllUniversitiesQuery, PageResponse>
{
    public async Task<Result<PageResponse>> Handle(GetAllUniversitiesQuery request, CancellationToken cancellationToken)
    {
        List<University> universities = await universityRepository.ListAsync(
            new GetUniversitiesSpec(
                request.Ids,
                request.Limit,
                request.Offset,
                request.SortBy,
                request.Ascending
            ),
            cancellationToken
        );
        int count = await universityRepository.CountAsync(
            new GetUniversitiesSpec(
                request.Ids,
                int.MaxValue,
                0
            ),
            cancellationToken
        );

        UniversityDto[] dtos = [..universities.Select(UniversityDto.FromEntity)];

        return new PageResponse(
            new(
                dtos.Length,
                count,
                request.Limit,
                request.Offset
            ),
            dtos
        );
    }
}
