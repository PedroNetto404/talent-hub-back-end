using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Queries.GetCandidateById;

public sealed class GetCandidateByIdQueryHandler(
    IRepository<Candidate> repository
) :
    IQueryHandler<GetCandidateByIdQuery, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        GetCandidateByIdQuery request,
        CancellationToken cancellationToken)
    {
        var candidate = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (candidate is null) return Error.Displayable("not_found", "candidate not found");

        return CandidateDto.FromEntity(candidate);
    }
}