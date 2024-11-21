namespace TalentHub.ApplicationCore.Shared.Dtos;

public record class PagedResponse<T>(
    Meta Meta,
    IEnumerable<T> Records
);

public sealed record Meta(
    int RecordCount,
    int Total,
    int Offset,
    int Limit
);