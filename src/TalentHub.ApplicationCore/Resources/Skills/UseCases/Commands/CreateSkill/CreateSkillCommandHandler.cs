using Ardalis.Specification;
using Humanizer;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Skills.Dtos;
using TalentHub.ApplicationCore.Resources.Skills.Enums;
using TalentHub.ApplicationCore.Resources.Skills.Specs;

namespace TalentHub.ApplicationCore.Resources.Skills.UseCases.Commands.CreateSkill;

public sealed class CreateSkillCommandHandler(
    IRepository<Skill> skillRepository
) : ICommandHandler<CreateSkillCommand, SkillDto>
{
    public async Task<Result<SkillDto>> Handle(
        CreateSkillCommand request, 
        CancellationToken cancellationToken
    )
    {
        Skill? existingSkill = await skillRepository.FirstOrDefaultAsync(new GetSkillByNameSpec(request.Name), cancellationToken);
        if (existingSkill is not null) 
        {
            return Error.BadRequest("skill alredy exists");
        }

        if (!Enum.TryParse(request.Type.Pascalize(), true, out SkillType skillType))
        {
            return new Error("skill", "invalid skill type");
        }

        Result<Skill> skillResult = Skill.Create(request.Name, skillType);
        if (skillResult.IsFail)
        {
            return skillResult.Error;
        }

        Skill skill = skillResult.Value;

        foreach (string tag in request.Tags)
        {
            if (skill.AddTag(tag) is { IsFail: true, Error: var tagError })
            {
                return tagError;
            }
        }

        await skillRepository.AddAsync(skill, cancellationToken);
        return SkillDto.FromEntity(skill);
    }
}
