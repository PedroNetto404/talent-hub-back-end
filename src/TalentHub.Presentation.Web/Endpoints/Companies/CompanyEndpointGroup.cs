using System;
using FastEndpoints;

namespace TalentHub.Presentation.Web.Endpoints.Companies;

public sealed class CompanyEndpointGroup : Group
{
    public CompanyEndpointGroup() =>
        Configure("companies", static ep => { });
}
