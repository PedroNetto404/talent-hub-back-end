using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Candidates.Specs;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Queries.GetAllCandidates;

public sealed class GetAllCandidatesQueryHandler(
    IRepository<Candidate> candidateRepository
) : IQueryHandler<GetAllCandidatesQuery, PagedResponse<CandidateDto>>
{
    public async Task<Result<PagedResponse<CandidateDto>>> Handle(
        GetAllCandidatesQuery request,
        CancellationToken cancellationToken
    )
    {
        List<Candidate> candidates = await candidateRepository.ListAsync(
            new GetAllCandidatesSpec(
                request.Limit,
                request.Offset,
                request.SortBy,
                request.Ascending,
                request.SkillIds,
                request.Languages),
            cancellationToken
        );

        int count = await candidateRepository.CountAsync(
            new GetAllCandidatesSpec(
                int.MaxValue,
                0,
                request.SortBy,
                request.Ascending,
                [],
                []),
            cancellationToken);

        CandidateDto[] dtos = [.. candidates.Select(CandidateDto.FromEntity)];

        return new PagedResponse<CandidateDto>(
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
