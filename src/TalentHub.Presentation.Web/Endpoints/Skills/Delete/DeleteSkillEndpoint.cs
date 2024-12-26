using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Skills.UseCases.Commands.DeleteSkill;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Skills.Delete;

public sealed class DeleteSkillEndpoint : Ep.Req<DeleteSkillRequest>.NoRes
{
    public override void Configure()
    {
        Delete("{skillId:guid}");
        Group<SKillEndpointGroup>();
        Version(1);
        Validator<DeleteSkillRequestValidator>();
    }

    public override Task HandleAsync(
        DeleteSkillRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(
        new DeleteSkillCommand(req.SkillId),
        ct
    );
}
