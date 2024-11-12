using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Candidates.UseCases.Queries.GetAllCandidates;
using TalentHub.Presentation.Web.Models.Request;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Candidates.UseCases.Queries.GetCandidateById;
using TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidateProfilePicture;
using TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidate;

namespace TalentHub.Presentation.Web.Controllers;

[Route("candidates")]
public sealed class CandidateController(
    ISender sender
) : ApiController
{
    #region Resource
    [HttpGet("{id:guid}")]
    public Task<IActionResult> GetByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default) =>
        sender.Send(
            new GetCandidateByIdQuery(id), 
            cancellationToken
        ).MatchAsync<CandidateDto, IActionResult>(Ok, BadRequest);

    [HttpGet]
    public Task<IActionResult> GetAllAsync(
        [FromQuery] PagedRequest request,
        CancellationToken cancellationToken) =>
        sender.Send(
            new GetAllCandidatesQuery(
                request.Limit,
                request.Offset,
                request.SortBy,
                request.SortAscending
            ), 
            cancellationToken
        ).MatchAsync<IEnumerable<CandidateDto>, IActionResult>(
            Ok,
            (err) => err.Code == "not_found" 
            ? NotFound(err)
            : BadRequest(err)
        );
            
    [HttpPost]
    public Task<IActionResult> CreateAsync(
        [FromBody] CreateCandidateRequest request,
        CancellationToken token = default) =>
        sender.Send(request.ToCommand(), token)
              .MatchAsync<CandidateDto, IActionResult>(Ok, BadRequest);

    [HttpPut("{id:guid}")]
    public Task<IActionResult> UpdateAsync(
        Guid id, 
        [FromBody] UpdateCandidateRequest request,
        CancellationToken token) =>
        sender.Send(
            new UpdateCandidateCommand(
                id,
                request.Name,
                request.Phone,
                request.Address,
                request.DesiredWorkplaceTypes,
                request.DesiredJobTypes,
                request.ExpectedRemuneration,
                request.InstagramUrl,
                request.LinkedinUrl,
                request.GithubUrl,
                request.Summary,
                request.ResumeFile?.OpenReadStream(),
                request.Hobbies),
            token
            ).MatchAsync<CandidateDto, IActionResult>(
                Ok,
                (err) => err.Code == "not_found"
                ? NotFound(err)
                : BadRequest(err)
            );

    [HttpPatch("profile_picture")]
    public async Task<IActionResult> UpdateProfilePictureAsync(
        Guid candidateId,
        [FromForm] IFormFile profilePicture,
        CancellationToken cancellationToken = default)
    {
        if(profilePicture.Length <= 0) 
            return BadRequest(Error.Displayable("candidate_profile_picture", "invalid file length"));

        return await sender.Send(
            new UpdateCandidateProfilePictureCommand(
                candidateId,
                profilePicture.OpenReadStream(),
                profilePicture.ContentType
            ),
            cancellationToken
        ).MatchAsync<CandidateDto, IActionResult>(
            Ok,
            (err) => err.Code == "not_found"
            ? NotFound(err)
            : BadRequest(err)
        );
    }
    #endregion

    #region Work Experiences Sub Resource
    #endregion

    #region Educational Experiences Sub Resource
    #endregion
}