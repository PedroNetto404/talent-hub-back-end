using TalentHub.ApplicationCore.Constants;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Delete;

public sealed class DeleteCandidateCommandHandler(
    IRepository<Candidate> repository,
    IFileStorage fileStorage
) : ICommandHandler<DeleteCandidateCommand>
{
    public async Task<Result> Handle(
        DeleteCandidateCommand request,
        CancellationToken cancellationToken)
    {
        Candidate? candidate = await repository.GetByIdAsync(
            request.CandidateId,
             cancellationToken
        );
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        if (candidate.ResumeUrl is not null)
        {
            await fileStorage.DeleteAsync(
                FileBucketNames.CandidateResumes,
                candidate.ResumeUrl.Split("/").Last(),
                cancellationToken
            );
        }

        await repository.DeleteAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}
