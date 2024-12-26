using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Skills.UseCases.Commands.UpdateSkill;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Skills.Update;

public sealed class UpdateSkillEndpoint :
    Ep.Req<UpdateSkillRequest>
      .NoRes
{
    public override void Configure()
    {
        Put("{skillId:guid}");
        Version(1);
        Validator<UpdateSkillRequestValidator>();
        Group<SKillEndpointGroup>();
    }

    public override Task HandleAsync(
        UpdateSkillRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(new UpdateSkillCommand(
            req.SkillId,
            req.Name,
            req.Type,
            req.Tags ?? []
        ),
        ct);
}
