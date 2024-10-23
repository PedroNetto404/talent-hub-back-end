using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Skills.Enums;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidateSkill;

public sealed record UpdateCandidateSkillCommand(
    Guid CandidateId,
    Guid CandidateSkillId,
    Proficiency? Proficiency,
    Dictionary<LanguageSkillType, Proficiency> SpecialProficientes
) : ICommand;

public sealed class UpdateCandidateSkillCommandHandler(
    IRepository<Candidate> repository
) :
    ICommandHandler<UpdateCandidateSkillCommand>
{
    public async Task<Result> Handle(
        UpdateCandidateSkillCommand request,
        CancellationToken cancellationToken)
    {
        var candidate = await repository.GetByIdAsync(request.CandidateId, cancellationToken);
        if (candidate is null) return Error.Displayable("not_found", "candidate not found");

        var skill = candidate.Skills.FirstOrDefault(s => s.Id == request.CandidateSkillId);
        if (skill is null) return Error.Displayable("not_found", "candidate skill not found");

        if (skill.SkillType != SkillType.Language)
        {
            candidate.UpdateSkillProficiency(skill.Id, request.Proficiency!.Value);
        }
        else
        {
            foreach (var specialLanguageSkill in request.SpecialProficientes)
            {
                candidate.UpdateLanguageSkillSpecialProficiency(
                    skill.Id,
                    specialLanguageSkill.Key,
                    specialLanguageSkill.Value
                );
            }
        }

        await repository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}