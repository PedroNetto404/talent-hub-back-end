using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Skills.Dtos;
using TalentHub.ApplicationCore.Resources.Skills.UseCases.Commands.CreateSkill;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Skills.Create;

public sealed class CreateSkillEndpoint
    : Ep.Req<CreateSkillRequest>.Res<SkillDto>
{
    public override void Configure()
    {
        Post("");
        Group<SKillEndpointGroup>();
        Version(1);
        Validator<CreateSkillRequestValidator>();
    }

    public override Task HandleAsync(
        CreateSkillRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(new CreateSkillCommand(
            req.Name,
            req.Type,
            req.Tags ?? []
        ),
        ct);
}
