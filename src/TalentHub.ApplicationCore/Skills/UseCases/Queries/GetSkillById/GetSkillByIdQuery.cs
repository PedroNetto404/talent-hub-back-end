using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Skills.UseCases.Dtos;

namespace TalentHub.ApplicationCore.Skills.UseCases.Queries.GetSkillById;

public sealed record GetSkillByIdQuery(Guid SkillId) : ICachedQuery<SkillDto>
{
    public TimeSpan? Duration => TimeSpan.FromHours(12);

    public string Key => nameof(GetSkillByIdQuery);
}

public sealed class GetSkillByIdQueryHandler(
    IRepository<Skill> skillRepository
) : IQueryHandler<GetSkillByIdQuery, SkillDto>
{
    public async Task<Result<SkillDto>> Handle(
        GetSkillByIdQuery request,
        CancellationToken cancellationToken)
    {
        var skill = await skillRepository.GetByIdAsync(request.SkillId, cancellationToken);
        if (skill is null) return NotFoundError.Value;

        return SkillDto.FromEntity(skill);
    }
}