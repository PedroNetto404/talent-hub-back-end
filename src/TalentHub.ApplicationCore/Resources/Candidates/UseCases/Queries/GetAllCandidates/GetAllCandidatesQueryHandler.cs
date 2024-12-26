using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetAllCandidates;

public sealed class GetAllCandidatesQueryHandler(
    IRepository<Candidate> repository
) : IQueryHandler<GetAllCandidatesQuery, PageResponse<CandidateDto>>
{
    public async Task<Result<PageResponse<CandidateDto>>> Handle(
        GetAllCandidatesQuery request,
        CancellationToken cancellationToken
    )
    {
        List<Candidate> candidates = await repository.ListAsync(
            new GetCandidatesSpec(
                request.SkillIds,
                request.Languages,
                request.Limit,
                request.Offset,
                request.SortBy,
                request.SortOrder
            ),
            cancellationToken
        );

        int count = await repository.CountAsync(
            new GetCandidatesSpec(
                request.SkillIds,
                request.Languages,
                int.MaxValue,
                0
            ),
            cancellationToken
        );

        CandidateDto[] dtos = [.. candidates.Select(CandidateDto.FromEntity)];

        return new PageResponse<CandidateDto>(
            new(
                dtos.Length,
                count,
                request.Offset,
                request.Limit
            ),
            dtos
        );
    }
}
