using System;
using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Resources.Universities.Specs;

public sealed class GetUniversityByNameSpec : Specification<University>
{
    public GetUniversityByNameSpec(string name)
    {
        Query.Where(u => u.Name == name).AsNoTracking();
    }
}
