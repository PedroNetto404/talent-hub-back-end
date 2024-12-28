using Microsoft.AspNetCore.Mvc;
using TalentHub.Presentation.Web.Shared.RequestModels;

namespace TalentHub.Presentation.Web.Endpoints.Companies.This.GetAll;

public record class GetAllCompaniesRequest(
    [property: FromQuery(Name = "name_like")] string? NameLike,
    [property: FromQuery(Name = "has_job_opening")] bool? HasJobOpening,
    [property: FromQuery(Name = "sector_id_in")] IEnumerable<Guid>? SectorIds,
    [property: FromQuery(Name = "location_like")] string? LocationLike
) : PageRequest;
