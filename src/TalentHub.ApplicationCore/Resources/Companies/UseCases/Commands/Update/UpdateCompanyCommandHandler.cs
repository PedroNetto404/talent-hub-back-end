using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.CompanySectors;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Update;

public sealed class UpdateCompanyCommandHandler(
    IRepository<Company> companyRepository,
    IRepository<CompanySector> companySectorRepository
) : ICommandHandler<UpdateCompanyCommand>
{
    public async Task<Result> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
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
                () => company.ChangeFacebookUrl(request.FacebookUrl),
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

        return Result.Ok();
    }
}
