using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Languages.UseCases.Create;
using TalentHub.Presentation.Web.Endpoints.Candidates.LanguageProficiences.Shared;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.LanguageProficiences.Create;

public sealed class CreateLanguageProficiencesEndpoint :
    Ep.Req<CreateLanguageProficiencesRequest>.Res<CandidateDto>
{
    public override void Configure()
    {
        Post("");
        Validator<CreateLanguageProficiencesRequestValidator>();
        Group<LanguageProficiencesEndpointSubGroup>();
        Version(1);
        Description(b =>
            b.Accepts<CreateLanguageProficiencesRequest>()
             .Produces<CandidateDto>(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status404NotFound));
    }

    public override async Task HandleAsync(
        CreateLanguageProficiencesRequest req,
        CancellationToken ct
    ) => await SendResultAsync(
        ResultUtils.Map(
            await Resolve<ISender>().Send(
                new CreateLanguageProficiencyCommand(
                    Route<Guid>("candidateId"),
                    req.WritingLevel,
                    req.ListeningLevel,
                    req.SpeakingLevel,
                    req.Language
                ),
                ct
            )
        )
    );
}
