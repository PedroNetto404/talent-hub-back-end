using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Resources.CompanySectors.Dtos;
using TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Commands.Create;
using TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Commands.Delete;
using TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Commands.Update;
using TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Queries.GetAll;
using TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Queries.GetById;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.Presentation.Web.Models.Request;

namespace TalentHub.Presentation.Web.Controllers;

[Route("api/company_sectors")]
public class CompanySectorController(ISender sender) : ApiController(sender)
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CompanySectorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken
    ) => HandleAsync<CompanySectorDto>(
        new GetCompanySectorByIdQuery(id),
        cancellationToken: cancellationToken
    );

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAllAsync(
        PagedRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync<PagedResponse>(
        new GetAllCompanySectorsQuery(
            request.Limit, 
            request.Offset, 
            request.SortBy, 
            request.Ascending
        ),
        cancellationToken: cancellationToken
    );

    [HttpPost]
    [ProducesResponseType(typeof(CompanySectorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateAsync(
        [FromBody] string name,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new CreateCompanySectorCommand(name),
        onSuccess: (dto) => Created($"api/company_sectors/{dto.Id}", dto),
        cancellationToken: cancellationToken
    );

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CompanySectorDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> UpdateAsync(
        Guid id,
        [FromBody] string name,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new UpdateCompanySectorCommand(id, name),
        cancellationToken: cancellationToken
    );

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new DeleteCompanySectorCommand(id),
        cancellationToken: cancellationToken,
        onSuccess: NoContent
    );
}
