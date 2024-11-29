using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Skills.UseCases.Commands.UpdateSkill;

public sealed class UpdateSkillCommandHandler(
    IRepository<Skill> skillRepository
) : ICommandHandler<UpdateSkillCommand>
{
    public async Task<Result> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await skillRepository.GetByIdAsync(request.Id, cancellationToken);
        if(skill is null) return NotFoundError.Value;

        skill.UpdateName(request.Name);

        skill.ClearTags();
        foreach(var tag in request.Tags) skill.AddTag(tag);

        await skillRepository.UpdateAsync(skill, cancellationToken);
        return Result.Ok();
    }
}