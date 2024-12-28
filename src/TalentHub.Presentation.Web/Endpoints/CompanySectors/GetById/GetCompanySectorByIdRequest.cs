using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.CompanySectors.GetById;

public sealed record GetCompanySectorByIdRequest(
    [property: FromRoute(Name = "companySectorId")] Guid Id
);
