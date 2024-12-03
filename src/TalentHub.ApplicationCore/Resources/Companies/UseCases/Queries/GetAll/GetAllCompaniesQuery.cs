using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.JobOpenings;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Queries.GetAll;

public sealed record GetAllCompaniesQuery(
    string? NameLike,
    bool? HasJobOpening,
    IEnumerable<Guid> SectorIds,
    string? LocationLike,
    int Limit,
    int Offset,
    string? SortBy,
    bool Ascending
) : IQuery<PagedResponse>;

public sealed class GetAllCompaniesQueryHandler(
    IRepository<Company> companyRepository,
    IRepository<JobOpening> jobOpeningRepository
) : IQueryHandler<GetAllCompaniesQuery, PagedResponse>
{
    public async Task<Result<PagedResponse>> Handle(
        GetAllCompaniesQuery request, 
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}
