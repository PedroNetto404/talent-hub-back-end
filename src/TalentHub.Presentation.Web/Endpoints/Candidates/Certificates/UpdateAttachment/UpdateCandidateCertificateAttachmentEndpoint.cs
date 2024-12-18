using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands.UpdateCertificateAttachment;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Certificates.UpdateAttachment;

public class UpdateCandidateCertificateAttachmentEndpoint : 
    Ep.Req<UpdateCandidateCertificateAttachmentRequest>
    .Res<CandidateDto>
{
    public override void Configure()
    {
        Patch("{certificateId:guid}/attachment");

        Description(builder => builder.Accepts<UpdateCandidateCertificateAttachmentRequest>()
            .Produces<CandidateDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Update a certificate attachment for a candidate.")
            .WithDisplayName("Update Candidate Certificate Attachment")
        );

        Validator<UpdateCandidateCertificateAttachmentRequestValidator>();

        Group<CandidateCertificatesEndpointsGroup>();

        Version(1);
    }

    public override async Task HandleAsync(UpdateCandidateCertificateAttachmentRequest req, CancellationToken ct)
    {
        (Guid candidateId, Guid certificateId) = (Route<Guid>("candidateId"), Route<Guid>("certificateId"));    
        
        using MemoryStream ms = new();
        await req.File.CopyToAsync(ms, ct);

        UpdateCandidateCertificateAttachmentCommand command = new(
            candidateId,
            certificateId,
            ms,
            req.File.ContentType
        );

         await SendResultAsync(
            ResultUtils.Map(
                await Resolve<ISender>().Send(command, ct)
            )
        );
    }
}
