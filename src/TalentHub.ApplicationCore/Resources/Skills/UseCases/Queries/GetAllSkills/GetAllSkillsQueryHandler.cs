using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Skills.Dtos;
using TalentHub.ApplicationCore.Skills.Specs;

namespace TalentHub.ApplicationCore.Skills.UseCases.Queries.GetAllSkills;

public sealed class GetAllSkillsQueryHandler(
    IRepository<Skill> skillRepository) : IQueryHandler<GetAllSkillsQuery, PagedResponse<SkillDto>>
{
    public async Task<Result<PagedResponse<SkillDto>>> Handle(
        GetAllSkillsQuery request,
        CancellationToken cancellationToken)
    {
        var skills = await skillRepository.ListAsync(
            new GetAllSkillsSpec(
                request.Limit,
                request.Offset,
                request.SortBy,
                request.Ascending,
                [..request.Ids]),
            cancellationToken);
        
        var count = await skillRepository.CountAsync(
            new GetAllSkillsSpec(
                int.MaxValue,
                0,
                request.SortBy,
                request.Ascending,
                request.Ids.ToArray()),
            cancellationToken);
        
        var dtos = skills.Select(SkillDto.FromEntity).ToArray();

        return new PagedResponse<SkillDto>(
            new(
                dtos.Length, 
                count, 
                request.Offset, 
                request.Limit),
            dtos);
    }
}