using Ardalis.Specification;

namespace TalentHub.ApplicationCore.EducationalInstitutes.Specs;

public sealed class GetEducationalInstitutesByIdsSpec : Specification<EducationalInstitute>
{
    public GetEducationalInstitutesByIdsSpec(IEnumerable<Guid> ids) =>
        Query.Where(institute => ids.Contains(institute.Id))
             .AsNoTracking();
}