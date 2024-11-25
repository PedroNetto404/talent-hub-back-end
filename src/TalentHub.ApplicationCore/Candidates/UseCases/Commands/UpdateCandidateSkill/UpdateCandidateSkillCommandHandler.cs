using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Skills;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidateSkill;

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
        var candidate = await candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
        if (candidate is null) return NotFoundError.Value;

        var skill = await skillRepository.GetByIdAsync(request.CandidateSkillId, cancellationToken);
        if (skill is null) return new Error("not_found", "candidate skill not found");

        candidate.UpdateSkillProficiency(skill.Id, request.Proficiency!.Value);

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}