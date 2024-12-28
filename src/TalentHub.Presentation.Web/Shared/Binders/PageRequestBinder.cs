using FastEndpoints;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Shared.Binders;

public abstract class PageRequestBinder<T> : IRequestBinder<T> where T : notnull
{
    public abstract ValueTask<T> BindAsync(BinderContext ctx, CancellationToken ct);

    protected static (int, int, string, SortOrder) GetPageRequest(IQueryCollection query)
    {
        int limit = query.Get<int?>("_limit") ?? 10;
        int offset = query.Get<int?>("_offset") ?? 0;
        string sortBy = query.Get<string?>("_sort_by") ?? "id";
        SortOrder sortOrder = query.GetEnum("_sort_order", SortOrder.Ascending);

        return (limit, offset, sortBy, sortOrder);
    }
}
