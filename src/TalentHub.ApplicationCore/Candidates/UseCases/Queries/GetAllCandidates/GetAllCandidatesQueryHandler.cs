using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Candidates.Specs;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Queries.GetAllCandidates;

public sealed class GetAllCandidatesQueryHandler(
    IRepository<Candidate> repository
) : IQueryHandler<GetAllCandidatesQuery, IEnumerable<CandidateDto>>
{
    public async Task<Result<IEnumerable<CandidateDto>>> Handle(
        GetAllCandidatesQuery request, 
        CancellationToken cancellationToken)
    {
        var candidates = await repository.ListAsync(
            new GetAllCandidatesSpec(
                request.Offset,
                request.Limit,
                request.SortBy,
                request.Ascending
            ),
            cancellationToken
        );

        return Result.Ok(candidates.Select(CandidateDto.FromEntity));
    }
}

