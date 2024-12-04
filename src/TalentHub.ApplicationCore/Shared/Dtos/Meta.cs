namespace TalentHub.ApplicationCore.Shared.Dtos;

public sealed record Meta(
    int RecordCount,
    int Total,
    int Offset,
    int Limit
)
{
    public bool Cached { get; init; } = false;
}