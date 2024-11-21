using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Candidates.Specs;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Skills;
using TalentHub.ApplicationCore.Skills.Specs;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Queries.GetAllCandidates;

public sealed class GetAllCandidatesQueryHandler(
    IRepository<Candidate> candidateRepository,
    IRepository<Skill> skillRepository
) : IQueryHandler<GetAllCandidatesQuery, PagedResponse<CandidateDto>>
{
    public async Task<Result<PagedResponse<CandidateDto>>> Handle(
        GetAllCandidatesQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new GetAllCandidatesSpec(
            request.Limit,
            request.Offset,
            request.SortBy,
            request.Ascending);

        var candidates = await candidateRepository.ListAsync(spec, cancellationToken);
        var count = await candidateRepository.CountAsync(spec, cancellationToken);

        var skillIds = candidates.SelectMany(p => p.Skills.Select(p => p.SkillId)).Distinct().ToArray();
        var skills = await skillRepository.ListAsync(new GetSkillsByIdsSpec(skillIds), cancellationToken);
        var candidateDtos = candidates.Select(candidate => CandidateDto.FromEntity(candidate, skills));

        return new PagedResponse<CandidateDto>(
            new(
                candidateDtos.Count(),
                count,
                request.Offset,
                request.Limit),
            candidateDtos
        );
    }
}

