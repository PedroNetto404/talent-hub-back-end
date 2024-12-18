using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills.UseCases.Commands.Delete;

public sealed class DeleteCandidateSkillCommandHandler(
    IRepository<Candidate> repository
) :
    ICommandHandler<DeleteCandidateSkillCommand>
{
    public async Task<Result> Handle(DeleteCandidateSkillCommand request, CancellationToken cancellationToken)
    {
        Candidate? candidate = await repository.GetByIdAsync(request.CandidateId, cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        if(candidate.RemoveSkill(request.CandidateSkillId) is
           {
               IsFail: true,
               Error: var error
           })
        {}

        await repository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}
