using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands.Delete;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Certificates.Delete;

public sealed class DeleteCertificateEndpoint : Ep.Req<DeleteCertificateRequest>.NoRes
{
    public override void Configure()
    {
        Delete("{certificateId:guid}");
        Group<CandidateCertificatesEndpointsSubGroup>();
        Version(1);
        Validator<DeleteCertificateRequestValidator>();
        Description(b => 
            b.Accepts<DeleteCertificateRequest>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
        );
    }

    public override async Task HandleAsync(DeleteCertificateRequest req, CancellationToken ct) =>
        await SendResultAsync(
            ResultUtils.Map(
                await Resolve<ISender>().Send(
                    new DeleteCandidateCertificateCommand(
                        req.CandidateId,
                        req.CertificateId
                    ),
                    ct
                )
            )
        );
}
