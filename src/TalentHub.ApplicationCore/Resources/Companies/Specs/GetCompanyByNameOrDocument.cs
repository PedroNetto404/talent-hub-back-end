using System;
using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Resources.Companies.Specs;

public sealed class GetCompanyByLegalNameOrCnpj : Specification<Company>
{
    public GetCompanyByLegalNameOrCnpj(
        string legalName,
        string cnpj
    )
    {
        Query.Where(c => c.LegalName == legalName);
        Query.Where(c => c.Cnpj == cnpj);
    }   
}
