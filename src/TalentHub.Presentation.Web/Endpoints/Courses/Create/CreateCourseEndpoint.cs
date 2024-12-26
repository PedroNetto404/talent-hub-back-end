using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Courses.Dtos;
using TalentHub.ApplicationCore.Resources.Courses.UseCases.Commands.Create;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Courses.Create;

public sealed class CreateCourseEndpoint :
    Ep.Req<CreateCourseRequest>.Res<CourseDto>
{
    public override void Configure()
    {
        Post("");
        Version(1);
        Group<CourseEndpointGroup>();
        Validator<CreateCourseRequestValidator>();
    }

    public override Task HandleAsync(CreateCourseRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new CreateCourseCommand(req.Name, req.Tags, req.RelatedSkillIds),
            ct
        );
}
