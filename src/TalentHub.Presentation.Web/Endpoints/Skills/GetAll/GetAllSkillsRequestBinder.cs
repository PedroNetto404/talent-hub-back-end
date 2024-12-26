using FastEndpoints;
using Humanizer;
using TalentHub.ApplicationCore.Resources.Skills.Enums;
using TalentHub.ApplicationCore.Shared.Enums;

public sealed class GetAllSkillsRequestBinder :
IRequestBinder<GetAllSkillsRequest>
{
    public ValueTask<GetAllSkillsRequest> BindAsync(
        BinderContext ctx,
        CancellationToken ct
    ) => ValueTask.FromResult(
        new GetAllSkillsRequest(
            ctx.HttpContext
                   .Request
                   .Query["skill_id_in"]
                   .FirstOrDefault()?
                   .Split(",")
                   .Select(Guid.Parse) ?? [],
                   ctx.HttpContext.Request.Query["type"].FirstOrDefault()
                   is string type ? Enum.Parse<SkillType>(type.Pascalize()) : null
        )
        {
            Limit = int.Parse(ctx.HttpContext.Request.Query["_limit"].FirstOrDefault() ?? "10"),
            Offset = int.Parse(ctx.HttpContext.Request.Query["_offset"].FirstOrDefault() ?? "0"),
            SortBy = ctx.HttpContext.Request.Query["_sort_by"].FirstOrDefault(),
            SortOrder = Enum.TryParse(
                ctx.HttpContext.Request.Query["_sort_order"].FirstOrDefault(),
                true,
                 out SortOrder sortOrder
            ) ? sortOrder : SortOrder.Ascending
        }
    );
}
