using TalentHub.ApplicationCore.Constants;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.UpdatePresentationVideo;

public sealed class UpdateCompanyPresentationVideoCommandHandler(
    IRepository<Company> companyRepository,
    IFileStorage fileStorage
) : ICommandHandler<UpdateCompanyPresentationVideoCommand, CompanyDto>
{
    public async Task<Result<CompanyDto>> Handle(
        UpdateCompanyPresentationVideoCommand request,
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

        if (company.PresentationVideoUrl is null)
        {
            return Error.InvalidInput("Company already has a presentation video");
        }

        string url = await fileStorage.SaveAsync(
            FileBucketNames.CompanyPresentationVideos,
            request.File,
            request.FileContentType,
            $"{company.PresentationVideoFileName}.{request.FileContentType.Split('/')[1]}",
            cancellationToken
        );

        if (company.ChangePresentationVideoUrl(url) is { IsFail: true, Error: var err })
        {
            return err;
        }

        await companyRepository.UpdateAsync(company, cancellationToken);

        return CompanyDto.FromEntity(company);
    }
}
