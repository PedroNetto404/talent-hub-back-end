using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.CreateCandidateCertificate;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateCandidateCertificate;
using TalentHub.Presentation.Web.Models.Request;

namespace TalentHub.Presentation.Web.Controllers;

[Route("api/candidates/{candidateId:guid}/certificates")]
public sealed class CandidateCertificatesController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    public Task<IActionResult> CreateAsync(
        Guid candidateId,
        CreateCandidateCertificateRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new CreateCandidateCertificateCommand(
            candidateId,
            request.Name,
            request.Issuer,
            request.Workload,
            request.Url,
            request.RelatedSkills),
        cancellationToken: cancellationToken);

    [HttpPut("{certificateId:guid}")]
    public Task<IActionResult> UpdateAsync(
        Guid candidateId,
        Guid certificateId,
        UpdateCertificateRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new UpdateCandidateCertificateCommand(
            candidateId,
            certificateId,
            request.Name,
            request.Issuer,
            request.Workload,
            request.Url,
            request.RelatedSkills
        ),
        cancellationToken: cancellationToken,
        onSuccess: NoContent
    );
}
