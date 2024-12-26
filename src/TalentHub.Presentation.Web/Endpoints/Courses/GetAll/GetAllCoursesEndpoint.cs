using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Courses.Dtos;
using TalentHub.ApplicationCore.Resources.Courses.UseCases.Queries.GetAll;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Courses.GetAll;

public sealed class GetAllCoursesEndpoint :
    Ep.Req<GetAllCoursesRequest>.Res<PageResponse<CourseDto>>
{
    public override void Configure()
    {
        Get("");
        Validator<GetAllCoursesRequestValidator>();
        Version(1);
        RequestBinder(new GetAllCoursesRequestBinder());
        Group<CourseEndpointGroup>();
    }

    public override Task HandleAsync(GetAllCoursesRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new GetAllCoursesQuery(
                req.NameLike,
                req.RelatedSkillIds ?? [],
                req.Limit ?? 10,
                req.Offset ?? 0,
                req.SortBy,
                req.SortOrder ?? ApplicationCore.Shared.Enums.SortOrder.Ascending
            ),
            ct
        );
}
