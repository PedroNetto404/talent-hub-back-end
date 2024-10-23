using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Candidates.UseCases.Queries.GetAllCandidates;
using TalentHub.Presentation.Web.Models.Request;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Candidates.UseCases.Queries.GetCandidateById;

namespace TalentHub.Presentation.Web.Controllers;

[Route("candidates")]
public sealed class CandidateController(
    ISender sender
) : ApiController
{
    [HttpGet("{id:guid}")]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid id) =>
        sender.Send(new GetCandidateByIdQuery(id))
              .MatchAsync<CandidateDto, IActionResult>(Ok, BadRequest);

    [HttpGet]
    public Task<IActionResult> GetAllAsync([FromQuery] PagedRequest request) =>
        sender.Send(new GetAllCandidatesQuery(
            request.Limit,
            request.Offset,
            request.SortBy,
            request.SortAscending
        )).MatchAsync<IEnumerable<CandidateDto>, IActionResult>
        (
            Ok,
            (err) => err.Code == "not_found" 
            ? NotFound(err)
            : BadRequest(err)
        );
            
    [HttpPost]
    public Task<IActionResult> CreateAsync([FromBody] CreateCandidateRequest request) =>
        sender.Send(request.ToCommand())
              .MatchAsync<CandidateDto, IActionResult>(Ok, BadRequest);
}