using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetAllCandidates;

public sealed class GetAllCandidatesQueryHandler(
    IRepository<Candidate> candidateRepository
) : IQueryHandler<GetAllCandidatesQuery, PageResponse>
{
    public async Task<Result<PageResponse>> Handle(
        GetAllCandidatesQuery request,
        CancellationToken cancellationToken
    )
    {
        List<Candidate> candidates = await candidateRepository.ListAsync(
            new GetCandidatesSpec(
                request.SkillIds,
                request.Languages,
                request.Limit,
                request.Offset,
                request.SortBy,
                request.Ascending
            ),
            cancellationToken
        );
        int count = await candidateRepository.CountAsync(
            new GetCandidatesSpec(
                request.SkillIds,
                request.Languages,
                int.MaxValue,
                0,
                null
            ), 
            cancellationToken
        );

        CandidateDto[] dtos = [.. candidates.Select(CandidateDto.FromEntity)];

        return new PageResponse(
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
