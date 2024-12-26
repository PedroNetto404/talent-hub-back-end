using System;
using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Companies.GetById;

public sealed record GetCompanyByIdRequest(
    [property: FromRoute(Name = "companyId")] Guid CompanyId
);
