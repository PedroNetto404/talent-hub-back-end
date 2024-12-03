using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.CreateAcademicExperience;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.DeleteExperience;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateAcademicExperience;
using TalentHub.Presentation.Web.Models.Request;

namespace TalentHub.Presentation.Web.Controllers;

[Route("api/candidates/{candidateId:guid}/experiences")]
[Authorize]
public sealed class CandidateExperienceController(
    ISender sender
) : ApiController(sender)
{
    [HttpPost("{type:alpha}")]
    [ProducesResponseType(typeof(ExperienceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> CreateAsync(
        Guid candidateId,
        string type,
        CreateExperienceRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new CreateExperienceCommand(
            candidateId,
            type,
            request.StartMonth,
            request.StartYear,
            request.EndMonth,
            request.EndYear,
            request.ExpectedGraduationStartMonth,
            request.ExpectedGraduationStartYear,
            request.IsCurrent,
            [.. request.Activities],
            request.Level,
            request.Status,
            request.CurrentSemester,
            [.. request.AcademicEntities],
            request.CourseId,
            request.UniversityId,
            request.Position,
            request.Description,
            request.Company,
            request.ProfessionalLevel
        ),
        (experience) => Created($"api/candidates/{candidateId}", experience),
        cancellationToken
    );

    [HttpPut("{type:alpha}/{experienceId:guid}")]
    [ProducesResponseType(typeof(ExperienceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> UpdateAsync(
        Guid candidateId,
        Guid experienceId,
        string type,
        UpdateExperienceRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new UpdateExperienceCommand(
            candidateId,
            experienceId,
            type,
            request.StartMonth,
            request.StartYear,
            request.EndMonth,
            request.EndYear,
            request.IsCurrent,
            [.. request.Activities],
            [.. request.AcademicEntities],
            request.CurrentSemester,
            request.Status,
            request.Description
        ),
        onSuccess: NoContent,
        cancellationToken
    );

    [HttpDelete("{experienceId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> DeleteAsync(
        Guid candidateId,
        Guid experienceId,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new DeleteExperienceCommand(candidateId, experienceId),
        onSuccess: NoContent,
        cancellationToken
    ); 
}
 