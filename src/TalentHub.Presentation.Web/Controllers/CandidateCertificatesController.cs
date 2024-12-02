using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.CreateCandidateCertificate;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.DeleteCandidateCertificate;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateCandidateCertificate;
using TalentHub.Presentation.Web.Models.Request;

namespace TalentHub.Presentation.Web.Controllers;

[Route("api/candidates/{candidateId:guid}/certificates")]
[Authorize]
public sealed class CandidateCertificatesController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    
    [ProducesResponseType(typeof(CandidateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    [HttpDelete("{certificateId:guid}")]
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> DeleteAsync(
        Guid candidateId,
        Guid certificateId,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new DeleteCandidateCertificateCommand(
            candidateId,
            certificateId
        ),
        cancellationToken: cancellationToken,
        onSuccess: NoContent
    );
}
