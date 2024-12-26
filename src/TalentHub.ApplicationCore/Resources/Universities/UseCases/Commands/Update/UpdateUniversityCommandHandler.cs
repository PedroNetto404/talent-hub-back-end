using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Universities.Dtos;

namespace TalentHub.ApplicationCore.Resources.Universities.UseCases.Commands.Update;

public sealed class UpdateUniversityCommandHandler(
    IRepository<University> universityRepository
) : ICommandHandler<UpdateUniversityCommand, UniversityDto>
{
    public async Task<Result<UniversityDto>> Handle(UpdateUniversityCommand request, CancellationToken cancellationToken)
    {
        University? university = await universityRepository.GetByIdAsync(request.Id, cancellationToken);
        if (university is null)
        {
            return Error.NotFound("university");
        }

        if (university.ChangeName(request.Name) is { IsFail: true, Error: var nameError })
        {
            return nameError;
        }

        if (
            request.SiteUrl is not null &&
            university.ChangeSiteUrl(request.SiteUrl) is { IsFail: true, Error: var siteUrlError }
        )
        {
            return siteUrlError;
        }

        await universityRepository.UpdateAsync(university, cancellationToken);
        return UniversityDto.FromEntity(university);
    }
}
