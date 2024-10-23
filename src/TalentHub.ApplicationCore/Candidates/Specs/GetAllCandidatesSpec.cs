using Ardalis.Specification;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Candidates.Specs;

public sealed class GetAllCandidatesSpec : PagedSpec<Candidate>
{
    public GetAllCandidatesSpec(
        int limit,
        int offset,
        string? sortBy,
        bool? ascending
    ) : base(limit, offset)
    {
        if(sortBy is null) return;

        var prop = typeof(Candidate).GetProperty(sortBy)
        ?? throw new InvalidOperationException($"property {sortBy} not found in candidate");

        if (ascending!.Value) Query.OrderBy(c => prop.GetValue(c));
        else Query.OrderByDescending(c => prop.GetValue(c));
    }
}