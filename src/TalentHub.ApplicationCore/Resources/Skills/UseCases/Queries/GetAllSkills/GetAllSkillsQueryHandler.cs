using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Skills.Dtos;
using TalentHub.ApplicationCore.Resources.Skills.Specs;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Skills.UseCases.Queries.GetAllSkills;

public sealed class GetAllSkillsQueryHandler(
    IRepository<Skill> skillRepository
) : IQueryHandler<GetAllSkillsQuery, PageResponse<SkillDto>>
{
    public async Task<Result<PageResponse<SkillDto>>> Handle(
        GetAllSkillsQuery request,
        CancellationToken cancellationToken)
    {
        List<Skill> skills = await skillRepository.ListAsync(
            new GetSkillsSpec(
                request.Ids,
                request.Type,
                request.Limit,
                request.Offset,
                request.SortBy,
                request.SortOrder
            ),
            cancellationToken
        );

        int count = await skillRepository.CountAsync(
            new GetSkillsSpec(
                request.Ids,
                request.Type,
                int.MaxValue,
                0
            ),
            cancellationToken
        );

        SkillDto[] dtos = [.. skills.Select(SkillDto.FromEntity)];
        return new PageResponse<SkillDto>(
            new(
                dtos.Length,
                count,
                request.Offset,
                request.Limit),
            dtos);
    }
}
