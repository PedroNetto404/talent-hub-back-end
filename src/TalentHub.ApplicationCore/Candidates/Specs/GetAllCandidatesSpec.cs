using System.Collections;
using Ardalis.Specification;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Candidates.Specs;

public sealed class GetAllCandidatesSpec : PagedSpec<Candidate>
{
    public GetAllCandidatesSpec(
        int limit,
        int offset,
        string? sortBy,
        bool ascending,
        IEnumerable<Guid> skillIds,
        IEnumerable<string> languages) : base(limit, offset)
    {
        if (sortBy is not null) Query.Sort(sortBy, ascending);

        (skillIds, languages) = (skillIds.ToList(), languages.ToList());

        if (skillIds is List<Guid> { Count: > 1 })
            Query.Where(p => 
                p.Skills.Any(sk => 
                    skillIds.Contains(sk.SkillId)));

        if (languages is List<Guid> { Count: > 1 })
            Query.Where(p => p.LanguageProficiencies.Any(l => languages.Contains(l.Language.Name)));
    }
}