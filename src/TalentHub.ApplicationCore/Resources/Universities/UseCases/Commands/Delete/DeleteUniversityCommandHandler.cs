using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Resources.Universities.UseCases.Commands.Delete;

public sealed class DeleteUniversityCommandHandler(
    IRepository<University> universityRepository
) : ICommandHandler<DeleteUniversityCommand>
{
    public async Task<Result> Handle(DeleteUniversityCommand request, CancellationToken cancellationToken)
    {
        University? university = await universityRepository.GetByIdAsync(request.Id, cancellationToken);
        if (university is null)
        {
            return Error.NotFound("university");
        }

        await universityRepository.DeleteAsync(university, cancellationToken);

        return Result.Ok();
    }
}
