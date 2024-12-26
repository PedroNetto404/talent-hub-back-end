using FastEndpoints;
using Humanizer;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.Presentation.Web.Endpoints.Companies.GetAll;

public sealed class GetAllCompaniesRequestBinder : IRequestBinder<GetAllCompaniesRequest>
{
    public ValueTask<GetAllCompaniesRequest> BindAsync(BinderContext ctx, CancellationToken ct)
    {
        IQueryCollection query = ctx.HttpContext.Request.Query;

        string? nameLike = GetQueryValue(query, "name_like");
        bool? hasJobOpening = GetBoolQueryValue(query, "has_job_opening");
        IEnumerable<Guid> sectorIds = GetGuidListQueryValue(query, "sector_id_in");
        string? locationLike = GetQueryValue(query, "location_like");

        int limit = GetIntQueryValue(query, "_limit", 10);
        int offset = GetIntQueryValue(query, "_offset", 0);
        string sortBy = GetQueryValue(query, "_sort_by") ?? "id";
        SortOrder sortOrder = GetEnumQueryValue(query, "_sort_order", SortOrder.Ascending);

        return ValueTask.FromResult(new GetAllCompaniesRequest(nameLike, hasJobOpening, sectorIds, locationLike)
        {
            Limit = limit,
            Offset = offset,
            SortBy = sortBy,
            SortOrder = sortOrder
        });
    }

    private static string? GetQueryValue(IQueryCollection query, string key)
    {
        return query[key].FirstOrDefault();
    }

    private static bool? GetBoolQueryValue(IQueryCollection query, string key)
    {
        return bool.TryParse(query[key].FirstOrDefault(), out bool result) ? result : (bool?)null;
    }

    private static IEnumerable<Guid> GetGuidListQueryValue(IQueryCollection query, string key)
    {
        return (query[key].FirstOrDefault()?.Split(",") ?? Array.Empty<string>()).Select(Guid.Parse);
    }

    private static int GetIntQueryValue(IQueryCollection query, string key, int defaultValue)
    {
        return int.TryParse(query[key].FirstOrDefault(), out int result) ? result : defaultValue;
    }

    private static SortOrder GetEnumQueryValue(IQueryCollection query, string key, SortOrder defaultValue)
    {
        return Enum.TryParse(query[key].FirstOrDefault()?.Pascalize(), true, out SortOrder result) ? result : defaultValue;
    }
}

