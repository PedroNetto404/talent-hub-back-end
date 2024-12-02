using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Skills.Dtos;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Skills.UseCases.Queries.GetAllSkills;

public sealed class GetAllSkillsQueryHandler(
    IRepository<Skill> skillRepository) : IQueryHandler<GetAllSkillsQuery, PagedResponse>
{
    public async Task<Result<PagedResponse>> Handle(
        GetAllSkillsQuery request,
        CancellationToken cancellationToken)
    {
        void additionalSpec(ISpecificationBuilder<Skill> query) =>
            query.Where(p => request.Ids.Contains(p.Id));
            
        List<Skill> skills = await skillRepository.GetPageAsync(
            request.Limit,
            request.Offset,
            request.SortBy,
            request.Ascending,
            additionalSpec,
            cancellationToken);
        
        int count = await skillRepository.CountAsync(
            additionalSpec,
            cancellationToken);
        
        SkillDto[] dtos = [.. skills.Select(SkillDto.FromEntity)];

        return new PagedResponse(
            new(
                dtos.Length, 
                count, 
                request.Offset, 
                request.Limit),
            dtos);
    }
}
