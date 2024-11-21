using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Skills;
using TalentHub.ApplicationCore.Skills.Specs;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Queries.GetCandidateById;

public sealed class GetCandidateByIdQueryHandler(
    IRepository<Candidate> candidateRepository,
    IRepository<Skill> skillRepository
) :
    IQueryHandler<GetCandidateByIdQuery, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        GetCandidateByIdQuery request,
        CancellationToken cancellationToken)
    {
        var candidate = await candidateRepository.GetByIdAsync(request.Id, cancellationToken);
        if (candidate is null) return NotFoundError.Value;

        return CandidateDto.FromEntity(
            candidate,
            await skillRepository.ListAsync(
                new GetSkillsByIdsSpec(candidate.Skills.Select(p => p.SkillId).ToArray()),
                cancellationToken
            )
        );
    }
}