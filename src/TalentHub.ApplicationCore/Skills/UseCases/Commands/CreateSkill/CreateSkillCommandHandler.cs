using Humanizer;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Skills.Enums;
using TalentHub.ApplicationCore.Skills.Specs;
using TalentHub.ApplicationCore.Skills.UseCases.Dtos;

namespace TalentHub.ApplicationCore.Skills.UseCases.Commands.CreateSkill;

public sealed class CreateSkillCommandHandler(
    IRepository<Skill> skillRepository
) : ICommandHandler<CreateSkillCommand, SkillDto>
{
    public async Task<Result<SkillDto>> Handle(
        CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var existingSkill = await skillRepository.FirstOrDefaultAsync(
            new GetSkillByNameSpec(request.Name), 
            cancellationToken);
        if (existingSkill is not null) return new Error("skill", "Skill already exists");

        if (!Enum.TryParse<SkillType>(request.Type.Pascalize(), true, out var skillType))
            return new Error("skill", "invalid skill type");

        var skillResult = Skill.Create(request.Name, skillType);
        if (skillResult.IsFail) return skillResult.Error;

        var skill = skillResult.Value;

        foreach (var tag in request.Tags)
            if (skill.AddTag(tag) is { IsFail: true, Error: var tagError })
                return tagError;

        await skillRepository.AddAsync(skill, cancellationToken);
        return SkillDto.FromEntity(skill);
    }
}
