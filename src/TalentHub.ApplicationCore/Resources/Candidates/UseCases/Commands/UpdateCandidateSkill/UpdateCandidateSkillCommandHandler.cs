using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Skills;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateCandidateSkill;

public sealed class UpdateCandidateSkillCommandHandler(
    IRepository<Candidate> candidateRepository,
    IRepository<Skill> skillRepository
) :
    ICommandHandler<UpdateCandidateSkillCommand>
{
    public async Task<Result> Handle(
        UpdateCandidateSkillCommand request,
        CancellationToken cancellationToken)
    {
        Candidate? candidate = await candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        Skill? skill = await skillRepository.GetByIdAsync(request.CandidateSkillId, cancellationToken);
        if (skill is null) 
        {
            return Error.NotFound("skill");
        }

        candidate.UpdateSkillProficiency(skill.Id, request.Proficiency!.Value);

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}
