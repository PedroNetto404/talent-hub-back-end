using FastEndpoints;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Courses.GetAll;

public sealed class GetAllCoursesRequestBinder : IRequestBinder<GetAllCoursesRequest>
{
  public ValueTask<GetAllCoursesRequest> BindAsync(BinderContext ctx, CancellationToken ct)
  {
    IQueryCollection query = ctx.HttpContext.Request.Query;

    int limit = query.Get("_limit", 10);
    int offset = query.Get("_offset", 0);
    string sortBy = query.Get("_sort_by", "id");
    SortOrder sortOrder = query.GetEnum("_sort_order", SortOrder.Ascending);

    string? nameLike = query.Get<string>("name_like", null!);
    IEnumerable<Guid>? relatedSkillIds = query.Get<IEnumerable<Guid>>("related_skill_id_in", null!);

    return ValueTask.FromResult(
        new GetAllCoursesRequest(
            nameLike,
             relatedSkillIds)
        {
          Limit = limit,
          Offset = offset,
          SortBy = sortBy,
          SortOrder = sortOrder
        });
  }
}
