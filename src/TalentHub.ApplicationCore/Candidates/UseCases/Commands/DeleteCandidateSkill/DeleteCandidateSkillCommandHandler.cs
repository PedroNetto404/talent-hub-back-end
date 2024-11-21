using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.DeleteCandidateSkill;

public sealed class DeleteCandidateSkillCommandHandler(
    IRepository<Candidate> repository
) :
    ICommandHandler<DeleteCandidateSkillCommand>
{
    public async Task<Result> Handle(DeleteCandidateSkillCommand request, CancellationToken cancellationToken)
    {
        var candidate = await repository.GetByIdAsync(request.CandidateId, cancellationToken);
        if(candidate is null) return NotFoundError.Value;

        candidate.RemoveSkill(request.CandidateSkillId);

        await repository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}