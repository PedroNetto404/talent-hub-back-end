using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Experiences.UseCases.Commands.DeleteExperience;

public sealed class DeleteExperienceCommandHandler(
    IRepository<Candidate> candidateRepository
) : ICommandHandler<DeleteExperienceCommand>
{
    public async Task<Result> Handle(
        DeleteExperienceCommand request,
        CancellationToken cancellationToken)
    {
        Candidate? candidate = await candidateRepository.FirstOrDefaultAsync(new GetCandidateByIdSpec(request.CandidateId), cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        if (candidate.RemoveExperience(request.ExperienceId) is { IsFail: true, Error: var error })
        {
            return error;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);

        return Result.Ok();
    }
}

