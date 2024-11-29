namespace TalentHub.ApplicationCore.Shared.Dtos;

public record class PagedResponse<T>(
    Meta Meta,
    IEnumerable<T> Records
) where T : notnull;

public sealed record Meta(
    int RecordCount,
    int Total,
    int Offset,
    int Limit
);
