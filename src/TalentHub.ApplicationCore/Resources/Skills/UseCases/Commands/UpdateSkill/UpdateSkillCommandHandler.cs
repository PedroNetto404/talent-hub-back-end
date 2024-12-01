using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

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
        (
            Guid id,
            string name,
            IEnumerable<string> tags
        ) = request;
        
        Skill? skill = await skillRepository.GetByIdAsync(id, cancellationToken);
        if(skill is null) 
        {
            return Error.NotFound("skill");
        }

        if(skill.UpdateName(name) is { IsFail: true, Error: var updateNameError})
        {
            return updateNameError;
        }

        skill.ClearTags();
        foreach(string tag in tags) 
        {
            if(skill.AddTag(tag) is { IsFail: true, Error: var addTagError})
            {
                return addTagError;
            }
        }

        await skillRepository.UpdateAsync(skill, cancellationToken);
        return Result.Ok();
    }
}
