using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Queries.GetById;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.Presentation.Web.Models.Request;
using System.ComponentModel.DataAnnotations;
using TalentHub.Presentation.Web.Binders;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Queries.GetAll;

namespace TalentHub.Presentation.Web.Controllers;

[Route("api/companies")]
[Authorize]
public sealed class CompanyController(ISender sender) : ApiController(sender)
{
    [HttpGet("{companyId:guid}")]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetByIdAsync(
        Guid companyId,
        CancellationToken cancellationToken
    ) => HandleAsync<CompanyDto>(
        new GetCompanyByIdQuery(companyId),
        cancellationToken: cancellationToken
    );

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAllAsync(
        GetAllCompaniesRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync<PagedResponse>(
        new GetAllCompaniesQuery(
            request.NameLike,
            request.HasJobOpenings,
            [.. request.SectorIds],
            request.LocationLike,
            request.Limit,
            request.Offset,
            request.SortBy,
            request.Ascending
        ),
        cancellationToken: cancellationToken
    );
}
