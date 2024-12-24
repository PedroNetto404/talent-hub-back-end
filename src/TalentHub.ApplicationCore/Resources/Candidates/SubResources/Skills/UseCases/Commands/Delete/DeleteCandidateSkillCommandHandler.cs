using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills.UseCases.Commands.Delete;

public sealed class DeleteCandidateSkillCommandHandler(
    IRepository<Candidate> repository
) :
    ICommandHandler<DeleteCandidateSkillCommand, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(DeleteCandidateSkillCommand request, CancellationToken cancellationToken)
    {
        Candidate? candidate = await repository.FirstOrDefaultAsync(new GetCandidateByIdSpec(request.CandidateId), cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        if (candidate.RemoveSkill(request.CandidateSkillId) is
            {
                IsFail: true,
                Error: var error
            })
        {
            return error;
        }

        await repository.UpdateAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}
