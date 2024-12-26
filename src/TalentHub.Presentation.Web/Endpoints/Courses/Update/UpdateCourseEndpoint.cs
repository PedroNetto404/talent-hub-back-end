using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Courses.Dtos;
using TalentHub.ApplicationCore.Resources.Courses.UseCases.Commands.Update;
using TalentHub.ApplicationCore.Resources.Skills.Dtos;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Courses.Update;

public sealed class UpdateCourseEndpoint :
    Ep.Req<UpdateCourseRequest>.Res<CourseDto>
{
    public override void Configure()
    {
        Put("{courseId:guid}");
        Group<CourseEndpointGroup>();
        Version(1);
        Validator<UpdateCourseRequestValidator>();
    }

    public override Task HandleAsync(UpdateCourseRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new UpdateCourseCommand(req.CourseId, req.Name, req.Tags, req.RelatedSkillIds),
            ct
        );
}
