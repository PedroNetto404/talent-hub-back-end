using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Universities.Dtos;
using TalentHub.ApplicationCore.Universities.Specs;

namespace TalentHub.ApplicationCore.Universities.UseCases.Queries.GetAll;

public sealed class GetAllUniversityQueryHandler(
    IRepository<University> universityRepository) : IQueryHandler<GetAllUniversitiesQuery, PagedResponse<UniversityDto>>
{
    public async Task<Result<PagedResponse<UniversityDto>>> Handle(GetAllUniversitiesQuery request, CancellationToken cancellationToken)
    {
        var universities = await universityRepository.ListAsync(
            new GetAllUniversitiesSpec(
                request.Limit,
                request.Offset,
                request.SortBy,
                request.Ascending,
                [.. request.Ids]
            ),
            cancellationToken);
        
        var count = await universityRepository.CountAsync(
            new GetAllUniversitiesSpec(
                int.MaxValue,
                0,
                request.SortBy,
                request.Ascending
            ),
            cancellationToken);
        
        var dtos = universities.Select(UniversityDto.FromEntity).ToList();

        return new PagedResponse<UniversityDto>(
            new(dtos.Count, count, request.Limit, request.Offset),
            dtos);
    }
}