using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Courses.GetById;

public sealed record GetCourseByIdRequest(
    [property: FromRoute(Name = "courseId")] Guid CourseId
);
