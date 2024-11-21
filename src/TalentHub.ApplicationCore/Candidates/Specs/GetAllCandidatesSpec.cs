using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Candidates.Specs;

public sealed class GetAllCandidatesSpec : PagedSpec<Candidate>
{
    public GetAllCandidatesSpec(
        int limit,
        int offset,
        string? sortBy,
        bool ascending = true
    ) : base(limit, offset)
    {
        if (sortBy is not null) Query.Sort(sortBy, ascending);
    }
}
