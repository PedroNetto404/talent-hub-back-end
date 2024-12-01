using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Skills.Dtos;

namespace TalentHub.ApplicationCore.Resources.Skills.UseCases.Queries.GetSkillById;

public sealed class GetSkillByIdQueryHandler(
    IRepository<Skill> skillRepository
) : IQueryHandler<GetSkillByIdQuery, SkillDto>
{
    public async Task<Result<SkillDto>> Handle(
        GetSkillByIdQuery request,
        CancellationToken cancellationToken)
    {
        Skill? skill = await skillRepository.GetByIdAsync(request.SkillId, cancellationToken);
        return skill is null
            ? Error.NotFound("skill")
            : SkillDto.FromEntity(skill);
    }
}
