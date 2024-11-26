namespace TalentHub.Presentation.Web.Models.Request;

public sealed record GetAllCoursesRequest : PagedRequest
{
    public IEnumerable<Guid> Ids { get; init; } = [];
}