using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Universities.Dtos;
using TalentHub.ApplicationCore.Universities.UseCases.Commands.Create;
using TalentHub.ApplicationCore.Universities.UseCases.Commands.Delete;
using TalentHub.ApplicationCore.Universities.UseCases.Commands.Update;
using TalentHub.ApplicationCore.Universities.UseCases.Queries.GetAll;
using TalentHub.ApplicationCore.Universities.UseCases.Queries.GetById;
using TalentHub.Presentation.Web.Binders;
using TalentHub.Presentation.Web.Models.Request;

namespace TalentHub.Presentation.Web.Controllers;

[Route("api/universities")]
public class UniversityController(ISender sender) : ApiController(sender)
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UniversityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    ) => HandleAsync<UniversityDto>(
        new GetUniversityByIdQuery(id),
        cancellationToken: cancellationToken
    );

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<UniversityDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAllAsync(
        [FromQuery(Name = "university_id_in"), ModelBinder(typeof(SplitQueryStringBinder))]
        IEnumerable<Guid> ids,
        PagedRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync<PagedResponse<UniversityDto>>(
        new GetAllUniversitiesQuery(
            ids,
            request.Limit,
            request.Offset,
            request.SortBy,
            request.Ascending
        ),
        cancellationToken: cancellationToken
    );

    [HttpPost]
    [ProducesResponseType(typeof(UniversityDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateAsync(
        CreateUniversityRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new CreateUniversityCommand(
            request.Name,
            request.SiteUrl
        ),
        cancellationToken: cancellationToken,
        onSuccess: dto => Created($"api/universities/{dto.Id}", dto)
    );

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> UpdateAsync(
        Guid id,
        UpdateUniversityRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new UpdateUniversityCommand(
            id,
            request.Name,
            request.SiteUrl
        ),
        onSuccess: NoContent,
        cancellationToken: cancellationToken
    );

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new DeleteUniversityCommand(
            id
        ),
        cancellationToken: cancellationToken,
        onSuccess: NoContent
    );
}
