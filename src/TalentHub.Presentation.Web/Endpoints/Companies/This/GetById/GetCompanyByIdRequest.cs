using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Companies.This.GetById;

public sealed record GetCompanyByIdRequest(
    [property: FromRoute(Name = "companyId")] Guid CompanyId
);
