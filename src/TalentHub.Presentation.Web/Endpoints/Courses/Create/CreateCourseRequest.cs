using FastEndpoints;

namespace TalentHub.Presentation.Web.Endpoints.Courses.Create;

public sealed record CreateCourseRequest(
    [property: FromBody] string Name,
    [property: FromBody] IEnumerable<string> Tags,
    [property: FromBody] IEnumerable<Guid> RelatedSkillIds
);
