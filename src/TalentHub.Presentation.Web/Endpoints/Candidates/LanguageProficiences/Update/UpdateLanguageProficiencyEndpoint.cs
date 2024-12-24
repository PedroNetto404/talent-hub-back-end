using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Languages.UseCases.Update;
using TalentHub.Presentation.Web.Endpoints.Candidates.LanguageProficiences.Shared;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.LanguageProficiences.Update;

public sealed class UpdateLanguageProficiencyEndpoint :
    Ep.Req<UpdateLanguageProficiencyRequest>.Res<CandidateDto>
{
    public override void Configure()
    {
        Put("{languageProficiencyId}");

        Description(b =>
            b.Accepts<CreateLanguageProficiencesRequest>()
             .Produces<CandidateDto>(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status404NotFound)
        );
        Version(1);
        Validator<UpdateLanguageProficiencyRequestValidator>();
        Group<LanguageProficiencesEndpointSubGroup>();
    }

    public override async Task HandleAsync(
        UpdateLanguageProficiencyRequest req,
        CancellationToken ct
    ) => await SendResultAsync(
        ResultUtils.Map(
            await Resolve<ISender>().Send(
                new UpdateLanguageProficiencyCommand(
                    Route<Guid>("candidateId"),
                    Route<Guid>("languageProficiencyId"),
                    req.WritingLevel,
                    req.ListeningLevel,
                    req.SpeakingLevel
                ), 
                ct
            )
        )
    );
}
