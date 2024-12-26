using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Skills.Dtos;
using TalentHub.ApplicationCore.Resources.Skills.UseCases.Queries.GetAllSkills;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Skills.GetAll;

public sealed class GetAllSkillsEndpoint :
    Ep.Req<GetAllSkillsRequest>.Res<PageResponse<SkillDto>>
{
    public override void Configure()
    {
        Get("");
        RequestBinder(new GetAllSkillsRequestBinder());
        Validator<GetAllSkillsRequestValidator>();
        Version(1);
        Group<SKillEndpointGroup>();
        Definition.AllowedRoles?.Clear();
    }

    public override Task HandleAsync(
        GetAllSkillsRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(
        new GetAllSkillsQuery(
            req.Ids ?? [],
            req.Type,
            req.Limit!.Value,
            req.Offset!.Value,
            req.SortBy,
            req.SortOrder!.Value
        ),
        ct);
}
