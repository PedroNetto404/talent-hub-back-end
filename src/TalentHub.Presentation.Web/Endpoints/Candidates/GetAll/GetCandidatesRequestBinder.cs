using FastEndpoints;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.GetAll;

public sealed class GetCandidatesRequestBinder : IRequestBinder<GetCandidatesRequest>
{
    public ValueTask<GetCandidatesRequest> BindAsync(BinderContext ctx, CancellationToken ct) => 
        ValueTask.FromResult(
            new GetCandidatesRequest(
                ctx.HttpContext.Request.Query["skill_id_in"].FirstOrDefault()?.Split(",")?.Select(Guid.Parse) ?? [],
                ctx.HttpContext.Request.Query["language_in"].FirstOrDefault()?.Split(",") ?? []
            )
            {
                Limit = int.Parse(ctx.HttpContext.Request.Query["_limit"].FirstOrDefault() ?? "10"),
                Offset = int.Parse(ctx.HttpContext.Request.Query["_offset"].FirstOrDefault() ?? "0"),
                SortBy = ctx.HttpContext.Request.Query["_sort_by"].FirstOrDefault() ?? "id",
                SortOrder = Enum.Parse<SortOrder>(ctx.HttpContext.Request.Query["_sort_order"].FirstOrDefault() ?? nameof(SortOrder.Ascending))
            }
        );
}
