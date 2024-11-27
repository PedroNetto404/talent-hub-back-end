using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Courses.Dtos;
using TalentHub.ApplicationCore.Courses.UseCases.Commands.Create;
using TalentHub.ApplicationCore.Courses.UseCases.Commands.Create.Delete;
using TalentHub.ApplicationCore.Courses.UseCases.Commands.Update;
using TalentHub.ApplicationCore.Courses.UseCases.Queries.GetAll;
using TalentHub.ApplicationCore.Courses.UseCases.Queries.GetById;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.Presentation.Web.Binders;
using TalentHub.Presentation.Web.Models.Request;

namespace TalentHub.Presentation.Web.Controllers;

[Route("api/courses")]
public sealed class CoursesController(ISender sender) : ApiController(sender)
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CourseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAllAsync(
        [FromQuery(Name = "course_id_in")] 
        [ModelBinder(typeof(SplitQueryStringBinder))]
        IEnumerable<Guid> ids,
        PagedRequest queryString,
        CancellationToken cancellationToken
    ) => HandleAsync<PagedResponse<CourseDto>>(
        new GetAllCoursesQuery(
            ids,
            queryString.Limit,
            queryString.Offset,
            queryString.SortBy,
            queryString.Ascending
        ),
        cancellationToken: cancellationToken
    );

    [HttpGet("{courseId:guid}")]
    [ProducesResponseType(typeof(CourseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetByIdAsync(
        Guid courseId, 
        CancellationToken cancellationToken) =>
        HandleAsync<CourseDto>(
            new GetCourseByIdQuery(courseId),
            cancellationToken: cancellationToken
        );

    [HttpPost]
    [ProducesResponseType(typeof(CourseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateAsync(
        CreateCourseRequest request,
        CancellationToken token
    ) =>
        HandleAsync(
            new CreateCourseCommand(
                request.Name,
                request.Tags,
                request.RelatedSkills
            ),
            cancellationToken: token,
            onSuccess: (course) => Created($"api/courses/{course.Id}", course)
        );

    [HttpPut("{courseId:guid}")]
    [ProducesResponseType(typeof(CourseDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> UpdateAsync(
        Guid courseId,
        UpdateCourseRequest request,
        CancellationToken cancellationToken
    ) =>
        HandleAsync(
            new UpdateCourseCommand(
                courseId,
                request.Name,
                request.Tags,
                request.RelatedSkills
            ),
            onSuccess: NoContent,
            cancellationToken: cancellationToken
        );

    [HttpDelete("{courseId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> DeleteAsync(
        Guid courseId,
        CancellationToken cancellationToken
    ) =>
        HandleAsync(
            new DeleteCourseCommand(
                courseId
            ),
            cancellationToken: cancellationToken,
            onSuccess: NoContent
        );
}