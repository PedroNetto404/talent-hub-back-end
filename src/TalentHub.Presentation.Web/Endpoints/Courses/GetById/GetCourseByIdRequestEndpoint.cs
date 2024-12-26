using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Courses.Dtos;
using TalentHub.ApplicationCore.Resources.Courses.UseCases.Queries.GetById;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Courses.GetById;

public sealed class GetCourseByIdRequestEndpoint :
    Ep.Req<GetCourseByIdRequest>.Res<CourseDto>
{
    public override void Configure()
    {
        Get("{courseId:guid}");
        Version(1);
        Group<CourseEndpointGroup>();
        Validator<GetCourseByIdRequestValidator>();
    }

    public override Task HandleAsync(GetCourseByIdRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new GetCourseByIdQuery(req.CourseId),
            ct
        );
}
