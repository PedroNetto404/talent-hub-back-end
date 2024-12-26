using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Universities.Dtos;
using TalentHub.ApplicationCore.Resources.Universities.Specs;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Universities.UseCases.Queries.GetAll;

public sealed class GetAllUniversityQueryHandler(
    IRepository<University> universityRepository
) : IQueryHandler<GetAllUniversitiesQuery, PageResponse<UniversityDto>>
{
    public async Task<Result<PageResponse<UniversityDto>>> Handle(
        GetAllUniversitiesQuery request, 
        CancellationToken cancellationToken
    )
    {
        List<University> universities = await universityRepository.ListAsync(
            new GetUniversitiesSpec(
                request.NameLike,
                request.Limit,
                request.Offset,
                request.SortBy,
                request.SortOrder
            ),
            cancellationToken
        );
        int count = await universityRepository.CountAsync(
            new GetUniversitiesSpec(
                request.NameLike,
                int.MaxValue,
                0
            ),
            cancellationToken
        );

        UniversityDto[] dtos = [.. universities.Select(UniversityDto.FromEntity)];

        return new PageResponse<UniversityDto>(
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
