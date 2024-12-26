using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Skills.Dtos;
using TalentHub.ApplicationCore.Resources.Skills.UseCases.Queries.GetSkillById;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Skills.GetById;

public sealed class GetSkillByIdEndpoint :
    Ep.Req<GetSkillByIdRequest>.Res<SkillDto>
{
    public override void Configure()
    {
        Get("{skillId:guid}");
        Version(1);
        Validator<GetSkillByIdRequestValidator>();
        Group<SKillEndpointGroup>();
        Definition.AllowedRoles?.Clear();
    }

    public override Task HandleAsync(
        GetSkillByIdRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(
        new GetSkillByIdQuery(req.SkillId),
        ct);
}
