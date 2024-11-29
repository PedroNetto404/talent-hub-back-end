using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Universities.Specs;

public sealed class GetUniversityByNameSpec : Specification<University>
{
    public GetUniversityByNameSpec(string name) =>
        Query.Where(institute => institute.Name == name)
            .AsNoTracking();
}