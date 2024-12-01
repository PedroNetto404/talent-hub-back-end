using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.DeleteExperience;

public sealed class DeleteExperienceCommandHandler(
    IRepository<Candidate> candidateRepository
) : ICommandHandler<DeleteExperienceCommand>
{
    public async Task<Result> Handle(
        DeleteExperienceCommand request, 
        CancellationToken cancellationToken)
    {
        Candidate? candidate = await candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        Result<Result> result = candidate.RemoveExperience(request.ExperienceId);
        if (result.IsFail)
        {}

        await candidateRepository.UpdateAsync(candidate, cancellationToken);

        return Result.Ok();
    }
}

