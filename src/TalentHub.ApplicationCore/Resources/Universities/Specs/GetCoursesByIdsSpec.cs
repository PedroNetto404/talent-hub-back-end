using Ardalis.Specification;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Universities.Specs;

public sealed class GetAllUniversitiesSpec : PagedSpec<University>
{
    public GetAllUniversitiesSpec(
        int limit, 
        int offset, 
        string? sortBy, 
        bool ascending,
        params Guid[] ids) : base(limit, offset, sortBy, ascending)
    {
        if (ids.Length != 0) 
            Query.Where(institute => ids.Contains(institute.Id));
        
        Query.AsNoTracking();
    }
}