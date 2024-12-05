using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Resources.CompanySectors;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Create;

public sealed class CreateCompanyCommandHandler(
    IRepository<Company> companyRepository,
    IRepository<CompanySector> companySectorRepository
) : ICommandHandler<CreateCompanyCommand, CompanyDto>
{
    public async Task<Result<CompanyDto>> Handle(
        CreateCompanyCommand request,
        CancellationToken cancellationToken
    )
    {
        Company? existingCompany = await companyRepository.FirstOrDefaultAsync(
            additionalSpec: (query) =>
                query.Where(company =>
                    company.LegalName == request.LegalName ||
                    company.Cnpj == request.Cnpj
                ),
            cancellationToken
        );
        if (existingCompany is not null)
        {
            return Error.BadRequest("company already exists");
        }

        CompanySector? sector = await companySectorRepository.GetByIdAsync(
            request.SectorId,
            cancellationToken
        );
        if (sector is null)
        {
            return Error.NotFound("company sector not found");
        }

        Result<Company> maybeCompany = Company.Create(
            request.LegalName,
            request.TradeName,
            request.Cnpj,
            request.About,
            sector.Id,
            request.RecruitmentEmail,
            request.Phone,
            request.AutoMatchEnabled,
            request.EmployeeCount,
            request.SiteUrl,
            request.Address,
            request.InstagramUrl,
            request.FacebookUrl,
            request.LinkedinUrl,
            request.CareerPageUrl,
            request.Mission,
            request.Vision,
            request.Values,
            request.FoundationYear
        );
        if (maybeCompany.IsFail)
        {
            return maybeCompany.Error;
        }

        await companyRepository.AddAsync(maybeCompany.Value, cancellationToken);
        return CompanyDto.FromEntity(maybeCompany.Value);

    }
}
