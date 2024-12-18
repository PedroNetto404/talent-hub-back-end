namespace TalentHub.ApplicationCore.Shared.Dtos;

public record class PageResponse(
    Meta Meta,
    IEnumerable<object> Records
)
{
    public PageResponse FromCache() =>
        new(
            Meta with { Cached = true },
            Records
        );
}
