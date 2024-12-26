using System.Collections;

namespace TalentHub.ApplicationCore.Shared.Dtos;

public interface IPageResponse
{
    public IPageResponse FromCache();
}

public record class PageResponse<T>(
    Meta Meta,
    IEnumerable<T> Records
) : IPageResponse
{
    public IPageResponse FromCache() =>
        new PageResponse<T>(
            Meta with { Cached = true },
            Records
        );
}
