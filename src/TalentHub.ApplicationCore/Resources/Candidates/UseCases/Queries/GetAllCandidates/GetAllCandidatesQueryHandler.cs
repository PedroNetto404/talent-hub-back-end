using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetAllCandidates;

public sealed class GetAllCandidatesQueryHandler(
    IRepository<Candidate> candidateRepository
) : IQueryHandler<GetAllCandidatesQuery, PagedResponse<CandidateDto>>
{
    public async Task<Result<PagedResponse<CandidateDto>>> Handle(
        GetAllCandidatesQuery request,
        CancellationToken cancellationToken
    )
    {
        void AdditionalSpec(ISpecificationBuilder<Candidate> query)
        {
            query.Where(p => p.Skills.Any(s => request.SkillIds.Contains(s.Id)));
            query.Where(p => p.LanguageProficiencies.Any(c => request.Languages.Contains(c.Language.Name)));
        }

        List<Candidate> candidates = await candidateRepository.GetPageAsync(
            request.Limit,
            request.Offset,
            request.SortBy,
            request.Ascending,
            additionalSpec: AdditionalSpec,
            cancellationToken
        );
        int count = await candidateRepository.CountAsync(AdditionalSpec, cancellationToken);

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
