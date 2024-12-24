using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Skills.Dtos;
using TalentHub.ApplicationCore.Resources.Skills.Specs;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Skills.UseCases.Queries.GetAllSkills;

public sealed class GetAllSkillsQueryHandler(
    IRepository<Skill> skillRepository) : IQueryHandler<GetAllSkillsQuery, PageResponse>
{
    public async Task<Result<PageResponse>> Handle(
        GetAllSkillsQuery request,
        CancellationToken cancellationToken)
    {
        List<Skill> skills = await skillRepository.ListAsync(
            new GetSkillsSpec(
                request.Ids,
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
                int.MaxValue,
                0
            ),
            cancellationToken
        );
        
        SkillDto[] dtos = [.. skills.Select(SkillDto.FromEntity)];
        return new PageResponse(
            new(
                dtos.Length, 
                count, 
                request.Offset, 
                request.Limit),
            dtos);
    }
}
