using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands.Create;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Certificates.Create;

public sealed class CreateCertificateEndpoint : Ep.Req<CreateCertificateRequest>.Res<CandidateDto>
{
    public override void Configure()
    {
        Post("");
        Description(builder => builder.Accepts<CreateCertificateRequest>()
            .Produces<CandidateDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Create a new certificate for a candidate.")
            .WithDisplayName("Create Candidate Certificate")
        );
        Version(1);
        Group<CandidateCertificatesEndpointsSubGroup>();
        Validator<CreateCertificateRequestValidator>();
    }

    public override async Task HandleAsync(CreateCertificateRequest req, CancellationToken ct) =>
        await SendResultAsync(
            ResultUtils.Map(
                await Resolve<ISender>().Send(
                    new CreateCandidateCertificateCommand(
                        req.CandidateId,
                        req.Name,
                        req.Issuer,
                        req.Workload,
                        req.RelatedSkills
                    ),
                    ct
                )
            )
        );
}
