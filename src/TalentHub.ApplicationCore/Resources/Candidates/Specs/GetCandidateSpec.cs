using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Resources.Candidates.Specs;

public sealed class GetCandidateByIdSpec : CandidateSpec
{
    public GetCandidateByIdSpec(Guid id) : base() => 
        Query.Where(c => c.Id == id);
}
