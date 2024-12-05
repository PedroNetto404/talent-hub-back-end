using TalentHub.ApplicationCore.Constants;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.RemoveLogo;

public sealed class RemoveCompanyLogoCommandHandler(
    IRepository<Company> companyRepository,
    IFileStorage fileStorage
) : ICommandHandler<RemoveCompanyLogoCommand, CompanyDto>
{
    public async Task<Result<CompanyDto>> Handle(RemoveCompanyLogoCommand request, CancellationToken cancellationToken)
    {
        Company? company = await companyRepository.GetByIdAsync(request.CompanyId, cancellationToken);
        if (company is null)
        {
            return Error.NotFound("company");
        }

        if (company.LogoUrl is null)
        {
            return Error.BadRequest("Company does not have a logo");
        }

        await fileStorage.DeleteAsync(
            FileBucketNames.CompanyLogos,
            company.LogoUrl,
            cancellationToken
        );

        await companyRepository.UpdateAsync(company, cancellationToken);

        return CompanyDto.FromEntity(company);
    }
}
