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
using TalentHub.ApplicationCore.Courses.Dtos;
using TalentHub.ApplicationCore.Courses.UseCases.Queries;
using TalentHub.ApplicationCore.Skills;
using TalentHub.Presentation.Web.Binders;

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
    ) => HandleAsync<CandidateDto>(
            new GetCandidateByIdQuery(id),
            cancellationToken: cancellationToken
        );

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<CandidateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAllAsync(
        PagedRequest request,
        [FromQuery(Name = "skill_id_in")]
        [ModelBinder(typeof(SplitQueryStringBinder))]
        IEnumerable<Guid> skillIds,
        [FromQuery(Name = "language_id_in")]
        [ModelBinder(typeof(SplitQueryStringBinder))]
        IEnumerable<string> languageIds,
        CancellationToken cancellationToken
    ) => HandleAsync<PagedResponse<CandidateDto>>(
            new GetAllCandidatesQuery(
                skillIds,
                languageIds,
                request.Limit,
                request.Offset,
                request.SortBy,
                request.Ascending),
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
