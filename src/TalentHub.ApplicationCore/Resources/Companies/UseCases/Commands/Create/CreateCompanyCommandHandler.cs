using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Resources.Companies.Specs;
using TalentHub.ApplicationCore.Resources.CompanySectors;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Create;

public sealed class CreateCompanyCommandHandler(
    IRepository<Company> companyRepository,
    IRepository<CompanySector> companySectorRepository,
    IUserContext userContext
) : ICommandHandler<CreateCompanyCommand, CompanyDto>
{
    public async Task<Result<CompanyDto>> Handle(
        CreateCompanyCommand request,
        CancellationToken cancellationToken
    )
    {
        if (userContext is not { IsCompany: true, UserId: { } userId })
        {
            return Error.Forbiden();
        }

        Company? existingCompany = await companyRepository.FirstOrDefaultAsync(
            new GetCompanyByLegalNameOrCnpjSpec(request.LegalName, request.Cnpj),
            cancellationToken
        );
        if (existingCompany is not null)
        {
            return Error.InvalidInput("company already exists");
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
            userId,
            request.RecruitmentEmail,
            request.Phone,
            request.AutoMatchEnabled,
            request.EmployeeCount,
            request.SiteUrl,
            request.Address,
            request.InstagramUrl,
            request.LinkedinUrl,
            request.CareerPageUrl,
            request.Mission,
            request.Vision,
            request.Values,
            request.FoundationYear);
        if (maybeCompany.IsFail)
        { return maybeCompany.Error; }

        await companyRepository.AddAsync(maybeCompany.Value, cancellationToken);
        return CompanyDto.FromEntity(maybeCompany.Value);
    }
}
