using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.JobOpportunities;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Queries.GetAll;

public sealed class GetAllCompaniesQueryHandler(
    IRepository<Company> companyRepository,
    IRepository<JobOpportunity> jobOpeningRepository
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
