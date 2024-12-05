using TalentHub.ApplicationCore.Constants;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.UpdateGaleryItem;

public sealed class UpdateCompanyGaleryCommandHandler(
    IFileStorage fileStorage,
    IRepository<Company> companyRepository
) : ICommandHandler<UpdateCompanyGaleryCommand, CompanyDto>
{
    public async Task<Result<CompanyDto>> Handle(
        UpdateCompanyGaleryCommand request,
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

        string url = await fileStorage.SaveAsync(
            FileBucketNames.CompanyGalery,
            request.File,
            request.FileContentType,
            $"{company.GetGaleryItemFileName()}.{request.FileContentType.Split('/')[1]}",
            cancellationToken
        );

        if (company.AddToGalery(url) is { IsFail: true, Error: var err })
        {
            return err;
        }

        await companyRepository.UpdateAsync(company, cancellationToken);

        return CompanyDto.FromEntity(company);
    }
}
