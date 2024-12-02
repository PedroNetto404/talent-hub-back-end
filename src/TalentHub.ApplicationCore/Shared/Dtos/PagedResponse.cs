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

public sealed record Meta(
    int RecordCount,
    int Total,
    int Offset,
    int Limit
)
{
    public bool Cached { get; init; } = false;
}
