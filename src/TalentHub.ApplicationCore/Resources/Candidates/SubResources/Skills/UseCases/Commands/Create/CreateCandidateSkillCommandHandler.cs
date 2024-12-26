using Humanizer;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetCandidateById;
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
        if (!Enum.TryParse(request.Proficiency.Pascalize(), true, out Proficiency proficiency))
        {
            return Error.InvalidInput($"{proficiency} is not valid proficiency");
        }

        Candidate? candidate = await candidateRepository.FirstOrDefaultAsync(new GetCandidateByIdSpec(request.CandidateId), cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        Skill? skill = await skillRepository.GetByIdAsync(request.SkillId, cancellationToken);
        if (skill is null)
        {
            return Error.NotFound("skill");
        }

        Result<CandidateSkill> candidateSkillResult = CandidateSkill.Create(skill.Id, proficiency);
        if (candidateSkillResult.IsFail)
        {
            return candidateSkillResult.Error;
        }

        if (candidate.AddSkill(candidateSkillResult.Value) is { IsFail: true, Error: var addError })
        {
            return addError;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}
