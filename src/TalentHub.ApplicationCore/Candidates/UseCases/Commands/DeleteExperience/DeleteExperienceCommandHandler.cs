
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.DeleteExperience;

public sealed class DeleteExperienceCommandHandler(
    IRepository<Candidate> candidateRepository
) : ICommandHandler<DeleteExperienceCommand>
{
    public async Task<Result> Handle(
        DeleteExperienceCommand request, 
        CancellationToken cancellationToken)
    {
        var candiate = await candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
        if (candiate is null) return NotFoundError.Value;

        var result = candiate.RemoveExperience(request.ExperienceId);
        if (result.IsFail) return result.Error;

        await candidateRepository.UpdateAsync(candiate, cancellationToken);

        return Result.Ok();
    }
}

