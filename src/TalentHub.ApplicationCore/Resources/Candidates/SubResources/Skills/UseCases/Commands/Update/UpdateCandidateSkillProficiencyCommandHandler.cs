using Humanizer;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills.UseCases.Commands.Update;

public sealed class UpdateCandidateSkillProficiencyCommandHandler(IRepository<Candidate> candidateRepository) :
    ICommandHandler<UpdateCandidateSkillProficiencyCommand, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        UpdateCandidateSkillProficiencyCommand request,
        CancellationToken cancellationToken
    )
    {
        Candidate? candidate = await candidateRepository.FirstOrDefaultAsync(new GetCandidateByIdSpec(request.CandidateId), cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        if (!Enum.TryParse(request.Proficiency.Pascalize(), true, out Proficiency proficiencyEnum))
        {
            return Error.InvalidInput($"{request.Proficiency} is not valid proficiency");
        }

        if (candidate.UpdateSkillProficiency(request.CandidateSkillId, proficiencyEnum) is
            {
                IsFail: true,
                Error: var err
            })
        {
            return err;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);

        return CandidateDto.FromEntity(candidate);
    }
}
