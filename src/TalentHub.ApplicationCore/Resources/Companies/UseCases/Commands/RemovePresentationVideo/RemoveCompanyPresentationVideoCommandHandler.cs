using TalentHub.ApplicationCore.Constants;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.RemovePresentationVideo;

public sealed class RemoveCompanyPresentationVideoCommandHandler(
    IRepository<Company> companyRepository,
    IFileStorage fileStorage
) : ICommandHandler<RemoveCompanyPresentationVideoCommand, CompanyDto>
{
    public async Task<Result<CompanyDto>> Handle(
        RemoveCompanyPresentationVideoCommand request,
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
            return Error.NotFound("presentation video");
        }

        await fileStorage.DeleteAsync(
            FileBucketNames.CompanyPresentationVideos,
            company.PresentationVideoUrl,
            cancellationToken
        );

        if (company.ChangePresentationVideoUrl(null) is { IsFail: true, Error: var err })
        {
            return err;
        }

        await companyRepository.UpdateAsync(company, cancellationToken);
        return CompanyDto.FromEntity(company);
    }
}
