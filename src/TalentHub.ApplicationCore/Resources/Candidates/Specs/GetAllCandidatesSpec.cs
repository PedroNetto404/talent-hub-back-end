using System;
using Ardalis.Specification;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Resources.Candidates.Specs;

public sealed class GetCandidatesSpec : GetPageSpec<Candidate>
{
    public GetCandidatesSpec(
        int limit,
        int offset,
        string? sortBy = null,
        bool ascending = true
    ) : base(limit, offset, sortBy, ascending)
    {
    }

    public GetCandidatesSpec(
        IEnumerable<Guid> skills,
        IEnumerable<string> languages,
        int limit,
        int offset,
        string? sortBy = null,
        bool ascending = true
    ) : this(limit, offset, sortBy, ascending)
    {
        if (skills.Any())
        {
            Query.Where(c => c.Skills.Any(s => skills.Contains(s.SkillId)));
        }

        if (languages.Any())
        {
            Query.Where(cl => cl.LanguageProficiencies.Any(
                l => languages.Contains(l.Language.Name)
            ));
        }
    }
}
