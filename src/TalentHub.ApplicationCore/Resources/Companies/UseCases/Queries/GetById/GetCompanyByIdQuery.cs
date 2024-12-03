using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Queries.GetById;

public sealed record GetCompanyByIdQuery(
    Guid CompanyId 
) : IQuery<CompanyDto>;

public sealed class GetCompanyByIdQueryHandler(
    IRepository<Company> companyRepository
) : IQueryHandler<GetCompanyByIdQuery, CompanyDto>
{
    public async Task<Result<CompanyDto>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        Company? company = await companyRepository.GetByIdAsync(request.CompanyId, cancellationToken);
        if(company is null)
        {
            return Error.NotFound("company");
        }

        return CompanyDto.FromEntity(company);
    }
}
