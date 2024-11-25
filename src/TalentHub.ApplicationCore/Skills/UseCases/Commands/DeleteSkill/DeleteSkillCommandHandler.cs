namespace TalentHub.ApplicationCore.Skills.UseCases.Commands.DeleteSkill;

using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

public sealed class DeleteSkillCommandHandler(
    IRepository<Skill> skillRepository
) : ICommandHandler<DeleteSkillCommand>
{
    public async Task<Result> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await skillRepository.GetByIdAsync(request.Id, cancellationToken);
        if (skill is null) return NotFoundError.Value;

        await skillRepository.DeleteAsync(skill, cancellationToken);

        return Result.Ok();
    }
}