using FastEndpoints;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.Presentation.Web.Extensions;
using TalentHub.Presentation.Web.Shared.Binders;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.This.GetAll;

public sealed class GetCandidatesRequestBinder : PageRequestBinder<GetCandidatesRequest>
{
    public override ValueTask<GetCandidatesRequest> BindAsync(BinderContext ctx, CancellationToken ct)
    {
        IQueryCollection query = ctx.HttpContext.Request.Query;

        IEnumerable<Guid> skillIds = query.Get<IEnumerable<Guid>>("skill_id_in", []);
        IEnumerable<string> languages = query.Get<IEnumerable<string>>("language_in", []);

        (int limit, int offset, string sortBy, SortOrder sortOrder) = GetPageRequest(query);

        return ValueTask.FromResult(new GetCandidatesRequest(skillIds, languages)
        {
            Limit = limit,
            Offset = offset,
            SortBy = sortBy,
            SortOrder = sortOrder
        });
    }
}
