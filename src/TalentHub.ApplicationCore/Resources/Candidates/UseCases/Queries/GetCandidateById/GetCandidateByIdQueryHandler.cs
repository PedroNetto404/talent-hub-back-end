using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetCandidateById;

public sealed class GetCandidateByIdQueryHandler(IRepository<Candidate> candidateRepository) :
    IQueryHandler<GetCandidateByIdQuery, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        GetCandidateByIdQuery request,
        CancellationToken cancellationToken)
    {
        Candidate? candidate = await candidateRepository.GetByIdAsync(request.Id, cancellationToken);
        if (candidate is null) 
        {
            return Error.NotFound("candidate");
        }

        return CandidateDto.FromEntity(candidate);
    }
}
