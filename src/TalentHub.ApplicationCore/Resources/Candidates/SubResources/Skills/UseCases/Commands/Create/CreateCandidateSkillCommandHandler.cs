using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Resources.Skills;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills.UseCases.Commands.Create;

public sealed class CreateCandidateSkillCommandHandler(
    IRepository<Candidate> candidateRepository,
    IRepository<Skill> skillRepository
) : ICommandHandler<CreateCandidateSkillCommand, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        CreateCandidateSkillCommand request,
        CancellationToken cancellationToken
    )
    {
        (Guid candidateId, Guid skillId, string proficiency) = request;

        if (!Enum.TryParse(proficiency, true, out Proficiency proficiency1))
        {
            return Error.BadRequest($"{proficiency} is not valid proficiency");
        }

        Candidate? candidate = await candidateRepository.GetByIdAsync(candidateId, cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        Skill? skill = await skillRepository.GetByIdAsync(skillId, cancellationToken);
        if (skill is null)
        {
            return Error.NotFound("skill");
        }

        Result<CandidateSkill> candidateSkillResult = CandidateSkill.Create(skill.Id, proficiency1);
        if(candidateSkillResult.IsFail)
        {
            return candidateSkillResult.Error;
        }

        if(candidate.AddSkill(candidateSkillResult.Value) is { IsFail: true, Error: var addError})
        {
            return addError;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}