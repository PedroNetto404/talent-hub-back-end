using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Universities.UseCases.Commands.Update;

public sealed class UpdateUniversityCommandHandler(
    IRepository<University> universityRepository
) : ICommandHandler<UpdateUniversityCommand>
{
    public async Task<Result> Handle(UpdateUniversityCommand request, CancellationToken cancellationToken)
    {
        University? university = await universityRepository.GetByIdAsync(request.Id, cancellationToken);
        if (university is null)
        { return NotFoundError.Value; }

        if (university.ChangeName(request.Name) is { IsFail: true, Error: var nameError })
        { return nameError; }

        if (
            request.SiteUrl is not null &&
            university.ChangeSiteUrl(request.SiteUrl) is { IsFail: true, Error: var siteUrlError }
        )
        { return siteUrlError; }

        await universityRepository.UpdateAsync(university, cancellationToken);
        return Result.Ok();
    }
}
