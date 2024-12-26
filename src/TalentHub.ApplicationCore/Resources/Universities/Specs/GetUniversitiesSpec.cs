using Ardalis.Specification;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Resources.Universities.Specs;

public sealed class GetUniversitiesSpec : GetPageSpec<University>
{
  public GetUniversitiesSpec(
      int limit,
      int offset,
      string? sortBy = null,
      SortOrder sortOrder = SortOrder.Ascending
  ) : base(limit, offset, sortBy, sortOrder) { }

  public GetUniversitiesSpec(
      string? nameLike,
      int limit,
      int offset,
      string? sortBy = null,
      SortOrder sortOrder = SortOrder.Ascending
  ) : this(limit, offset, sortBy, sortOrder)
  {
    if (!string.IsNullOrWhiteSpace(nameLike))
    {
      Query.Where(u => u.Name.Contains(nameLike));
    }
  }
}
