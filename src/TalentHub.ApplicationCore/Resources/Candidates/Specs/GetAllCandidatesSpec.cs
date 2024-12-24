using Ardalis.Specification;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Resources.Candidates.Specs;

public sealed class GetCandidatesSpec : CandidateSpec
{
    public GetCandidatesSpec(
        int limit,
        int offset,
        string? sortBy = null,
        SortOrder sortOrder = SortOrder.Ascending
    ) : base()
    {
        Query.Paginate(limit, offset);

        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            Query.Sort(sortBy, sortOrder);
        }
    }

    public GetCandidatesSpec(
        IEnumerable<Guid> skills,
        IEnumerable<string> languages,
        int limit,
        int offset,
        string? sortBy = null,
        SortOrder sortOrder = SortOrder.Ascending
    ) : this(limit, offset, sortBy, sortOrder)
    {
        if (skills.Any())
        {
            Query.Where(c => c.Skills.Any(s => skills.Contains(s.SkillId)));
        }

        if (languages.Any())
        {
            Language[] langs = [.. languages.Select(l => Language.FromName(l, true))];
            Query.Where(c => c.LanguageProficiencies.Any(lp => langs.Contains(lp.Language)));
        }
    }
}
