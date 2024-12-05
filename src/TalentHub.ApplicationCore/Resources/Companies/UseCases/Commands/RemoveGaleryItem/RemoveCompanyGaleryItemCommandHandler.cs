using TalentHub.ApplicationCore.Constants;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.RemoveGaleryItem;

public sealed class RemoveCompanyGaleryItemCommandHandler(
    IRepository<Company> companyRepository,
    IFileStorage fileStorage
) : ICommandHandler<RemoveCompanyGaleryItemCommand, CompanyDto>
{
    public async Task<Result<CompanyDto>> Handle(
        RemoveCompanyGaleryItemCommand request,
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

        if (company.RemoveFromGalery(request.Url) is { IsFail: true, Error: var err })
        {
            return err;
        }

        string fileName = company.GetGaleryItemFileName(request.Url);
        await fileStorage.DeleteAsync(
            FileBucketNames.CompanyGalery,
            fileName,
            cancellationToken
        );

        await companyRepository.UpdateAsync(company, cancellationToken);

        return CompanyDto.FromEntity(company);
    }
}
