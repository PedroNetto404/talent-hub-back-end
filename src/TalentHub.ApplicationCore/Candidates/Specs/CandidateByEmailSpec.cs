using System;
using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Candidates.Specs;

public sealed class CandidateByEmailSpec : SingleResultSpecification<Candidate>
{
    public CandidateByEmailSpec(string email) => 
        Query.Where(c => c.Email == email).AsNoTracking();
}
