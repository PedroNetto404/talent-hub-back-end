using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Courses.Update;

public sealed record UpdateCourseRequest(
    [property: FromRoute(Name = "courseId")] Guid CourseId,
    [property: FromBody] string Name,
    [property: FromBody] IEnumerable<string> Tags,
    [property: FromBody] IEnumerable<Guid> RelatedSkillIds
);
