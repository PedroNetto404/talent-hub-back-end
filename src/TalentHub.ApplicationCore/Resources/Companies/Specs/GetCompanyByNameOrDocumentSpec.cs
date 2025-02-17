using System;
using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Resources.Companies.Specs;

public sealed class GetCompanyByLegalNameOrCnpjSpec : Specification<Company>
{
    public GetCompanyByLegalNameOrCnpjSpec(
        string legalName,
        string cnpj
    )
    {
        Query.Where(c => c.LegalName == legalName);
        Query.Where(c => c.Cnpj == cnpj);
    }
}
