using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Universities.Dtos;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Universities.UseCases.Queries.GetAll;

public sealed class GetAllUniversityQueryHandler(
    IRepository<University> universityRepository) : IQueryHandler<GetAllUniversitiesQuery, PagedResponse<UniversityDto>>
{
    public async Task<Result<PagedResponse<UniversityDto>>> Handle(GetAllUniversitiesQuery request, CancellationToken cancellationToken)
    {
        void additionalSpec(ISpecificationBuilder<University> query) => 
            query.Where(u => request.Ids.Contains(u.Id));

        List<University> universities = await universityRepository.GetPageAsync(
            request.Limit,
            request.Offset,
            request.SortBy,
            request.Ascending,
            additionalSpec: additionalSpec,
            cancellationToken);
        int count = await universityRepository.CountAsync(
            additionalSpec,
            cancellationToken);

        var dtos = universities.Select(UniversityDto.FromEntity).ToList();

        return new PagedResponse<UniversityDto>(
            new(dtos.Count, count, request.Limit, request.Offset),
            dtos);
    }
}
