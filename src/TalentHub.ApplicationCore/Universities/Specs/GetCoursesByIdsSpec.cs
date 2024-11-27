using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Universities.Specs;

public sealed class GetUniversitiesByIdsSpec : Specification<University>
{
    public GetUniversitiesByIdsSpec(IEnumerable<Guid> ids) =>
        Query.Where(institute => ids.Contains(institute.Id))
             .AsNoTracking();
}