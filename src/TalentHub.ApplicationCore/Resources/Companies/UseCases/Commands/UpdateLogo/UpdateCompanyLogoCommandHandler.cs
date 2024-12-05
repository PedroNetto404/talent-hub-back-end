using TalentHub.ApplicationCore.Constants;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.UpdateLogo;

public sealed class UpdateCompanyLogoCommandHandler(
    IRepository<Company> companyRepository,
    IFileStorage fileStorage
) : ICommandHandler<UpdateCompanyLogoCommand, CompanyDto>
{
    public async Task<Result<CompanyDto>> Handle(
        UpdateCompanyLogoCommand request,
        CancellationToken cancellationToken
    )
    {
        Company? company = await companyRepository.GetByIdAsync(
            request.CompanyId,
            cancellationToken
        );
        if (company is null)
        {
            return Error.NotFound("company");
        }

        if (company.LogoUrl is not null)
        {
            await fileStorage.DeleteAsync(
                FileBucketNames.CompanyLogos,
                company.LogoFileName,
                cancellationToken
            );
        }

        string url = await fileStorage.SaveAsync(
            FileBucketNames.CompanyLogos,
            request.File,
            company.LogoFileName,
            request.FileContentType,
            cancellationToken
        );

        if (company.ChangeLogoUrl(url) is { IsFail: true, Error: var err })
        {
            return err;
        }

        await companyRepository.UpdateAsync(company, cancellationToken);

        return CompanyDto.FromEntity(company);
    }
}
