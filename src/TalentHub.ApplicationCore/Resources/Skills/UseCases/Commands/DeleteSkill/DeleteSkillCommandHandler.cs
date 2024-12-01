using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Resources.Skills.UseCases.Commands.DeleteSkill;

public sealed class DeleteSkillCommandHandler(
    IRepository<Skill> skillRepository
) : ICommandHandler<DeleteSkillCommand>
{
    public async Task<Result> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        Skill? skill = await skillRepository.GetByIdAsync(request.Id, cancellationToken);
        if (skill is null)
        {
            return Error.NotFound("skill");
        }

        await skillRepository.DeleteAsync(skill, cancellationToken);

        return Result.Ok();
    }
}
