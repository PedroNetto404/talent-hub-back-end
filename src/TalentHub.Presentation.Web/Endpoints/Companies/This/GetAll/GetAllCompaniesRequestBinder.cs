using FastEndpoints;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.Presentation.Web.Extensions;
using TalentHub.Presentation.Web.Shared.Binders;

namespace TalentHub.Presentation.Web.Endpoints.Companies.This.GetAll;

public sealed class GetAllCompaniesRequestBinder : PageRequestBinder<GetAllCompaniesRequest>
{
    public override ValueTask<GetAllCompaniesRequest> BindAsync(BinderContext ctx, CancellationToken ct)
    {
        IQueryCollection query = ctx.HttpContext.Request.Query;

        string? nameLike = query.Get<string?>("name_like");
        bool? hasJobOpening = query.Get<bool?>("has_job_opening");
        IEnumerable<Guid> sectorIds = query.Get<IEnumerable<Guid>?>("sector_id_in") ?? [];
        string? locationLike = query.Get<string?>("location_like");

        (
            int limit,
            int offset,
            string sortBy,
            SortOrder sortOrder
        ) = GetPageRequest(query);

        return ValueTask.FromResult(new GetAllCompaniesRequest(
            nameLike,
            hasJobOpening,
            sectorIds,
            locationLike)
        {
            Limit = limit,
            Offset = offset,
            SortBy = sortBy,
            SortOrder = sortOrder
        });
    }
}

