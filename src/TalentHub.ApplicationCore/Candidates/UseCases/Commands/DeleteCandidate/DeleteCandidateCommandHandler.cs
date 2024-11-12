
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.DeleteCandidate;

public sealed record DeleteCandidateCommand(Guid CandidateId) : ICommand;

public sealed class DeleteCandidateCommandHandler(
    IRepository<Candidate> repository,
    IFileStorage fileStorage
) : ICommandHandler<DeleteCandidateCommand>
{
    public async Task<Result> Handle(
        DeleteCandidateCommand request, 
        CancellationToken cancellationToken)
    {
        var candidate = await repository.GetByIdAsync(request.CandidateId, cancellationToken);
        if(candidate is null) return Error.Displayable("not_found", "candidate not found");

        await Task.WhenAll(
            candidate.ResumeUrl is not null 
            ? fileStorage.DeleteAsync(candidate.ResumeFileName, cancellationToken) 
            : Task.CompletedTask,
            candidate.ProfilePictureUrl is not null 
            ? fileStorage.DeleteAsync(candidate.ProfilePictureFileName, cancellationToken)
            : Task.CompletedTask
        );

        await repository.DeleteAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}
