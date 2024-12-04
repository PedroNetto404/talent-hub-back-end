using Microsoft.AspNetCore.Mvc;
using TalentHub.Presentation.Web.Binders;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record GetAllCompaniesRequest : PagedRequest
{
    [FromQuery(Name = "name_like")]
    public string? NameLike { get; init; }

    [FromQuery(Name = "has_job_openings")]
    public bool? HasJobOpenings { get; init; }

    [FromQuery(Name = "sector_id_in")]
    [ModelBinder(typeof(SplitQueryStringBinder))]
    public IEnumerable<Guid> SectorIds { get; init; } = [];

    [FromQuery(Name = "location_like")]
    public string? LocationLike { get; init; }
}