using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands.Delete;
using TalentHub.Presentation.Web.Extensions;

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

    public override Task HandleAsync(DeleteCertificateRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new DeleteCandidateCertificateCommand(
                req.CandidateId,
                req.CertificateId
            ),
            ct
        );
}
