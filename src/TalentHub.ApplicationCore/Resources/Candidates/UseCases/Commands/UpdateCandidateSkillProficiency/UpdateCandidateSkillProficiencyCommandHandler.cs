using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateCandidateSkillProficiency;

public sealed class UpdateCandidateSkillProficiencyCommandHandler(IRepository<Candidate> candidateRepository) :
    ICommandHandler<UpdateCandidateSkillProficiencyCommand>
{
    public async Task<Result> Handle(UpdateCandidateSkillProficiencyCommand request, CancellationToken cancellationToken)
    {
        (Guid candidateId, Guid candidateSkillId, string proficiency) = request;

        Candidate? candidate = await candidateRepository.GetByIdAsync(candidateId, cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        if(!Enum.TryParse(proficiency, true, out Proficiency proficiencyEnum))
        {
            return Error.BadRequest($"{proficiency} is not valid proficiency");
        }

        if(candidate.UpdateSkillProficiency(candidateSkillId, proficiencyEnum) is 
        {
            IsFail: true,
            Error: var err
        })
        {
            return err;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);

        return Result.Ok();
    }
}
