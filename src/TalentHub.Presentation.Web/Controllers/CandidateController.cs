using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Candidates.UseCases.Commands.DeleteCandidate;
using TalentHub.ApplicationCore.Candidates.UseCases.Queries.GetAllCandidates;
using TalentHub.Presentation.Web.Models.Request;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Candidates.UseCases.Queries.GetCandidateById;
using TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidateProfilePicture;
using TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidate;
using TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidateResume;
using TalentHub.ApplicationCore.Shared.Dtos;
using System.Net.Mime;

namespace TalentHub.Presentation.Web.Controllers;

[Route("api/candidates")]
public sealed class CandidatesController(ISender sender) : ApiController(sender)
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CandidateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    ) =>
        HandleAsync<CandidateDto>(
            new GetCandidateByIdQuery(id),
            cancellationToken: cancellationToken
        );

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<CandidateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAllAsync(
        PagedRequest request,
        CancellationToken cancellationToken
    ) =>
        HandleAsync<PagedResponse<CandidateDto>>(
            new GetAllCandidatesQuery(
                request.Limit,
                request.Offset,
                request.SortBy,
                request.SortAscending),
            cancellationToken: cancellationToken
        );

    [HttpPost]
    [ProducesResponseType(typeof(CandidateDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateAsync(
        CreateCandidateRequest request,
        CancellationToken token = default
    ) =>
        HandleAsync(
            request.ToCommand(),
            (candidate) => Created($"api/candidates/{candidate.Id}", candidate),
            cancellationToken: token
         );

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CandidateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateCandidateRequest request,
        CancellationToken cancellationToken
    ) =>
        HandleAsync(
            new UpdateCandidateCommand(
                id,
                request.Name,
                request.Phone,
                request.Address,
                [.. request.DesiredWorkplaceTypes],
                [.. request.DesiredJobTypes],
                request.ExpectedRemuneration,
                request.InstagramUrl,
                request.LinkedinUrl,
                request.GithubUrl,
                request.Summary,
                [.. request.Hobbies]),
            onSuccess: NoContent,
            cancellationToken
        );

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default
    ) =>
        HandleAsync(
            new DeleteCandidateCommand(id),
            onSuccess: NoContent,
            cancellationToken
        );

    [HttpPut("{id:guid}/profile_picture")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(CandidateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProfilePictureAsync(
        Guid id,
        IFormFile file,
        CancellationToken cancellationToken = default)
    {
        if (file is not { Length: > 0 }) return BadRequest(new Error("bad_request", "No file uploaded."));


        if (file.ContentType != MediaTypeNames.Image.Jpeg && file.ContentType != MediaTypeNames.Image.Png)
            return BadRequest(new Error("candidate_profile_picture", "invalid file type"));

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken);

        return await HandleAsync(
            new UpdateCandidateProfilePictureCommand(
                id,
                stream,
                file.ContentType),
                cancellationToken: cancellationToken
        );
    }

    [HttpPut("{id:guid}/resume")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateResumeAsync(
        [FromRoute] Guid id,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        if (file is not { Length: > 0 }) return BadRequest("No file uploaded.");
        if (!Path.GetExtension(file.FileName).Equals(".pdf", StringComparison.CurrentCultureIgnoreCase))
            return BadRequest("Only PDF files are allowed.");

        using var stream = new MemoryStream();

        await file.CopyToAsync(stream, cancellationToken);

        return await HandleAsync(
            new UpdateCandidateResumeCommand(id, stream),
            cancellationToken: cancellationToken
        );
    }
}

[Route("api/candidates/{candidateId:guid}/academic_experiences")]
public sealed class CandidateAcademicExperienceController 
{
    [HttpPost]
    [ProducesResponseType(typeof(AcademicExperienceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> CreateAsync(
        Guid candidateId,
        CreateAcademicExperienceRequest request,
        CancellationToken cancellationToken
    ) =>
        HandleAsync(
            new CreateAcademicExperienceCommand(
                candidateId,
                request.Start,
                request.End,
                request.IsCurrent,
                request.Level,
                request.Status,
                request.CourseId,
                request.InstitutionId
            ),
            (experience) => Created($"api/candidates/{candidateId}/academic_experiences/{experience.Id}", experience),
            cancellationToken
        );

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AcademicExperienceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> UpdateAsync(
        Guid candidateId,
        Guid id,
        UpdateAcademicExperienceRequest request,
        CancellationToken cancellationToken
    ) =>
        HandleAsync(
            new UpdateAcademicExperienceCommand(
                candidateId,
                id,
                request.Start,
                request.End,
                request.IsCurrent,
                request.Level,
                request.Status,
                request.CourseId,
                request.InstitutionId
            ),
            onSuccess: NoContent,
            cancellationToken
        );

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> DeleteAsync(
        Guid candidateId,
        Guid id,
        CancellationToken cancellationToken
    ) =>
        HandleAsync(
            new DeleteAcademicExperienceCommand(candidateId, id),
            onSuccess: NoContent,
            cancellationToken
        );
}

public sealed class CandidateProfessionalExperienceController
{
    [HttpPost]
    [ProducesResponseType(typeof(ProfessionalExperienceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> CreateAsync(
        Guid candidateId,
        CreateProfessionalExperienceRequest request,
        CancellationToken cancellationToken
    ) =>
        HandleAsync(
            new CreateProfessionalExperienceCommand(
                candidateId,
                request.Start,
                request.End,
                request.IsCurrent,
                request.Position,
                request.Company,
                request.Industry,
                request.Description
            ),
            (experience) => Created($"api/candidates/{candidateId}/professional_experiences/{experience.Id}", experience),
            cancellationToken
        );

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProfessionalExperienceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> UpdateAsync(
        Guid candidateId,
        Guid id,
        UpdateProfessionalExperienceRequest request,
        CancellationToken cancellationToken
    ) =>
        HandleAsync(
            new UpdateProfessionalExperienceCommand(
                candidateId,
                id,
                request.Start,
                request.End,
                request.IsCurrent,
                request.Position,
                request.Company,
                request.Industry,
                request.Description
            ),
            onSuccess: NoContent,
            cancellationToken
        );

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> DeleteAsync(
        Guid candidateId,
        Guid id,
        CancellationToken cancellationToken
    ) =>
        HandleAsync(
            new DeleteProfessionalExperienceCommand(candidateId, id),
            onSuccess: NoContent,
            cancellationToken
        );
}