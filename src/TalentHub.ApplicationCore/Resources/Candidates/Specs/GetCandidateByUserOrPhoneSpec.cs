using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Resources.Candidates.Specs;

public sealed class GetCandidateByUserOrPhoneSpec : CandidateSpec
{
    public GetCandidateByUserOrPhoneSpec(Guid userId, string phone) => 
        Query.Where(c => c.UserId == userId || c.Phone == phone)
             .AsNoTracking();
}
