using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Candidates.UseCases.Commands.DeleteCandidateSkill;
using TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidateSkill;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.Presentation.Web.Models.Request;

namespace TalentHub.Presentation.Web.Controllers;

[Route("candidates/{candidateId:guid}/skills")]
public sealed class CandidateSkillController(
    ISender sender
) : ApiController
{
    [HttpPut("{candidateSkillId:guid}")]
    public Task<IActionResult> UpdateAsync(
        Guid candidateId,
        Guid candidateSkillId,
        [FromBody] UpdateCandidateSkillRequest request) =>
        sender.Send(new UpdateCandidateSkillCommand(
            candidateId,
            candidateSkillId,
            request.Proficiency,
            request.SpecialProficiences
        )).MatchAsync<IActionResult>(
            NoContent,
            BadRequest
        );

    [HttpDelete("{candidateSkillId:guid}")]
    public Task<IActionResult> DeleteAsync(
        Guid candidateId,
        Guid candidateSkillId
    ) =>
        sender.Send(new DeleteCandidateSkillCommand(
            candidateId,
            candidateSkillId
        )).MatchAsync<IActionResult>(
            NoContent,
            BadRequest
        );
}