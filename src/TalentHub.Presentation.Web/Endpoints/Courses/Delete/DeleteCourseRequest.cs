using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Courses.Delete;

public sealed record DeleteCourseRequest(
    [property: FromRoute(Name = "courseId")] Guid CourseId
);
