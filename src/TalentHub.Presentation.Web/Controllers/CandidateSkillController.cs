using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.CreateCandidateSkill;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.DeleteCandidateSkill;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateCandidateSkillProficiency;
using TalentHub.Presentation.Web.Models.Request;

namespace TalentHub.Presentation.Web.Controllers;

[Route("api/candidates/{candidateId:guid}/skills")]
[Authorize]
public sealed class CandidateSkillController(ISender sender) : ApiController(sender)
{ 
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> CreateAsync(
        Guid candidateId,
        CreateCandidateSkillRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new CreateCandidateSkillCommand(
            candidateId,
            request.SkillId,
            request.Proficiency
        ),
        cancellationToken: cancellationToken
    );

    [HttpPatch("{candidateSkillId:guid}/proficiency")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> UpdateProficiencyAsync(
        Guid candidateId,
        Guid candidateSkillId,
        [FromBody, AllowedValues("beginner", "intermediate", "advanced")]
        string proficiency,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new UpdateCandidateSkillProficiencyCommand(
            candidateId,
            candidateSkillId,
            proficiency
        ),
        cancellationToken: cancellationToken,
        onSuccess: NoContent
    );

    [HttpDelete("{candidateSkillId:guid}")]

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> DeleteAsync(
        Guid candidateId,
        Guid candidateSkillId,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new DeleteCandidateSkillCommand(candidateId, candidateSkillId),
        cancellationToken: cancellationToken,
        onSuccess: NoContent
    );
}
