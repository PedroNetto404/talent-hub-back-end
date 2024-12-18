using System;
using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.Specs;

public sealed class GetCompanySectorByName : Specification<CompanySector>
{   
    public GetCompanySectorByName(string name)
    {
        Query.Where(cs => cs.Name == name).AsNoTracking();
    }
}
