using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Resources.Companies.Specs;
using TalentHub.ApplicationCore.Resources.CompanySectors;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Update;

public sealed class UpdateCompanyCommandHandler(
    IRepository<Company> companyRepository,
    IRepository<CompanySector> companySectorRepository
) : ICommandHandler<UpdateCompanyCommand, CompanyDto>
{
    public async Task<Result<CompanyDto>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        Company? existingCompany = await companyRepository.FirstOrDefaultAsync(
            new GetCompanyByLegalNameOrCnpjSpec(request.LegalName, request.Cnpj),
            cancellationToken
        );
        if(existingCompany is not null && existingCompany.Id != request.CompanyId)
        {
            return Error.InvalidInput("legal name or cnpj already in use");
        }

        Company? company = await companyRepository.GetByIdAsync(request.CompanyId, cancellationToken);
        if (company is null)
        {
            return Error.NotFound("company");
        }

        CompanySector? companySector = await companySectorRepository.GetByIdAsync(request.SectorId, cancellationToken);
        if (companySector is null)
        {
            return Error.NotFound("company_sector");
        }

        if (
            Result.FailEarly(
                () => company.ChangeLegalName(request.LegalName),
                () => company.ChangeTradeName(request.TradeName),
                () => company.ChangeCnpj(request.Cnpj),
                () => company.ChangeAbout(request.About),
                () => company.ChangeSectorId(companySector.Id),
                () => company.ChangePhone(request.Phone),
                () => company.ChangeRecruitmentEmail(request.RecruitmentEmail),
                () => company.ChangeEmployeeCount(request.EmployeeCount),
                () => company.ChangeSiteUrl(request.SiteUrl),
                () => company.ChangeInstagramUrl(request.InstagramUrl),
                () => company.ChangeLinkedinUrl(request.LinkedinUrl),
                () => company.ChangeCareerPageUrl(request.CareerPageUrl),
                () => company.ChangeMission(request.Mission),
                () => company.ChangeVision(request.Vision),
                () => company.ChangeValues(request.Values),
                () => company.ChangeFoundantionYear(request.FoundantionYear)
            ) is { IsFail: true, Error: var err }
        )
        {
            return err;
        }

        company.SetAutoMatchEnabled(request.AutoMatchEnabled);
        company.ChangeAddress(request.Address);

        await companyRepository.UpdateAsync(company, cancellationToken);

        return CompanyDto.FromEntity(company);
    }
}
