using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Courses.UseCases.Commands.Create.Delete;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Courses.Delete;

public sealed class DeleteCourseEndpoint :
    Ep.Req<DeleteCourseRequest>.NoRes
{
  public override void Configure()
  {
    Delete("{courseId:guid}");
    Validator<DeleteCourseRequestValidator>();
    Version(1);
    Group<CourseEndpointGroup>();
  }

  public override Task HandleAsync(DeleteCourseRequest req, CancellationToken ct) =>
      this.HandleUseCaseAsync(
          new DeleteCourseCommand(req.CourseId),
          ct
      );
}
