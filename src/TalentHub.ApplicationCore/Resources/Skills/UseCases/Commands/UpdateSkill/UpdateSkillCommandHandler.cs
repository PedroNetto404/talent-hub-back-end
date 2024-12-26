using Humanizer;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Skills.Enums;
using TalentHub.ApplicationCore.Resources.Skills.Specs;

namespace TalentHub.ApplicationCore.Resources.Skills.UseCases.Commands.UpdateSkill;

public sealed class UpdateSkillCommandHandler(
    IRepository<Skill> skillRepository
) : ICommandHandler<UpdateSkillCommand>
{
    public async Task<Result> Handle(
        UpdateSkillCommand request,
        CancellationToken cancellationToken
    )
    {
        Skill? existingSkill = await skillRepository.FirstOrDefaultAsync(
            new GetSkillByNameSpec(request.Name),
            cancellationToken
        );
        if (existingSkill is not null)
        {
            return Error.InvalidInput("Skill with the same name already exists");
        }

        Skill? skill = await skillRepository.GetByIdAsync(request.SkillId, cancellationToken);
        if (skill is null)
        {
            return Error.NotFound("skill");
        }

        if (!Enum.TryParse(request.Type.Pascalize(), true, out SkillType skillType))
        {
            return Error.InvalidInput($"{request.Type} is not valid skill type");
        }
        skill.ChangeType(skillType);

        if (skill.UpdateName(request.Name) is { IsFail: true, Error: var updateNameError })
        {
            return updateNameError;
        }

        skill.ClearTags();
        foreach (string tag in request.Tags)
        {
            if (skill.AddTag(tag) is { IsFail: true, Error: var addTagError })
            {
                return addTagError;
            }
        }

        await skillRepository.UpdateAsync(skill, cancellationToken);
        return Result.Ok();
    }
}
