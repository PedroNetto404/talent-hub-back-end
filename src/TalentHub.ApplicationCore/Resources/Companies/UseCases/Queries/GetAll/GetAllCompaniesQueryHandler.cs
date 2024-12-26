using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Resources.Jobs;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Queries.GetAll;

public sealed class GetAllCompaniesQueryHandler(
    IRepository<Company> companyRepository,
    IRepository<Job> jobOpeningRepository
) : IQueryHandler<GetAllCompaniesQuery, PageResponse<CompanyDto>>
{
    public async Task<Result<PageResponse<CompanyDto>>> Handle(
        GetAllCompaniesQuery request, 
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}
