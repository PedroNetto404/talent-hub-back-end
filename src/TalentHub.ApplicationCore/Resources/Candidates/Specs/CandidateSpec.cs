using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Resources.Candidates.Specs;

public abstract class CandidateSpec : Specification<Candidate>
{
    protected CandidateSpec() => 
        Query.Include(p => p.Skills)
            .Include(p => p.LanguageProficiencies)
            .Include(p => p.Certificates)
            .Include(p => p.Experiences);
}
