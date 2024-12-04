using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Shared.Dtos;

public record class PagedResponse(
    Meta Meta,
    IEnumerable<object> Records
)
{
    public PagedResponse FromCache() =>
        new(
            Meta with { Cached = true },
            Records
        );
}