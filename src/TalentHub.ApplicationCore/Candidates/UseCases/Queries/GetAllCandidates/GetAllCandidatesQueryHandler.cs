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
        CancellationToken cancellationToken)
    {
        var spec = new GetAllCandidatesSpec(
            request.Limit,
            request.Offset,
            request.SortBy,
            request.Ascending);

        var candidates = await candidateRepository.ListAsync(spec, cancellationToken);
        var count = await candidateRepository.CountAsync(spec, cancellationToken);

        var candidateDtos = candidates.Select(CandidateDto.FromEntity);

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

