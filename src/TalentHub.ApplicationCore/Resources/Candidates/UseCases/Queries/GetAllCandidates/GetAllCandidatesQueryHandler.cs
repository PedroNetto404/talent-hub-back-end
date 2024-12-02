using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetAllCandidates;

public sealed class GetAllCandidatesQueryHandler(
    IRepository<Candidate> candidateRepository
) : IQueryHandler<GetAllCandidatesQuery, PagedResponse>
{
    public async Task<Result<PagedResponse>> Handle(
        GetAllCandidatesQuery request,
        CancellationToken cancellationToken
    )
    {
        (
            IEnumerable<Guid> skillIds,
            IEnumerable<string> languages,
            int limit,
            int offset,
            string? sortBy,
            bool ascending
        ) = request;

        void AdditionalSpec(ISpecificationBuilder<Candidate> query)
        {
            if (skillIds.Any())
            {
                query.Where(p => p.Skills.Any(s => request.SkillIds.Contains(s.Id)));
            }

            if (languages.Any())
            {
                query.Where(c => c.LanguageProficiencies.Any(
                    l => languages.Contains(l.Language.Name)
                ));
            }
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

        return new PagedResponse(
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
