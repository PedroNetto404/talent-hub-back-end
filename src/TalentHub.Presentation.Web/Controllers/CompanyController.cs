using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Queries.GetById;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.Presentation.Web.Models.Request;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.RemoveLogo;
using TalentHub.ApplicationCore.Core.Results;
using System.Net.Mime;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.UpdateLogo;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.UpdatePresentationVideo;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.RemovePresentationVideo;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.UpdateGaleryItem;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.RemoveGaleryItem;

namespace TalentHub.Presentation.Web.Controllers;

[Route("api/companies")]
[Authorize]
public sealed class CompanyController(ISender sender) : ApiController(sender)
{
    private static readonly string[] allowedVideoExtensions = [".mp4", ".avi", ".mov", ".mkv"];

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
        request.ToQuery(),
        cancellationToken: cancellationToken
    );

    [HttpPost]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateAsync(
        CreateCompanyRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync(
        request.ToCommand(),
        onSuccess: (dto) => Created($"api/companies/{dto.Id}", dto),
        cancellationToken: cancellationToken
    );

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> UpdateAsync(
        UpdateCompanyRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync(
        request.ToCommand(),
        cancellationToken: cancellationToken
    );

    [HttpPatch("{companyId:guid}/logo")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateLogoAsync(
        Guid companyId,
        IFormFile file,
        CancellationToken cancellationToken
    )
    {
        if (file is not { Length: > 0 })
        {
            return BadRequest(Error.BadRequest("No file uploaded."));
        }

        if (file.ContentType is not MediaTypeNames.Image.Jpeg and not MediaTypeNames.Image.Png)
        {
            return BadRequest(Error.BadRequest("Invalid file type."));
        }

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken);

        return await HandleAsync(
            new UpdateCompanyLogoCommand(companyId, stream, file.ContentType),
            cancellationToken: cancellationToken
        );
    }

    [HttpDelete("{companyId:guid}/logo")]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> RemoveLogoAsync(
        Guid companyId,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new RemoveCompanyLogoCommand(companyId),
        cancellationToken: cancellationToken
    );

    [HttpPatch("{companyId:guid}/presentation_video")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePresentationVideoAsync(
        Guid companyId,
        IFormFile file,
        CancellationToken cancellationToken
    )
    {
        if (file is not { Length: > 0 })
        {
            return BadRequest(Error.BadRequest("No file uploaded."));
        }

        if (allowedVideoExtensions.Contains(Path.GetExtension(file.FileName)) is false)
        {
            return BadRequest(Error.BadRequest("Invalid file type."));
        }

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken);

        return await HandleAsync(
            new UpdateCompanyPresentationVideoCommand(companyId, stream, file.ContentType),
            cancellationToken: cancellationToken
        );
    }

    [HttpDelete("{companyId:guid}/presentation_video")]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> RemovePresentationVideoAsync(
        Guid companyId,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new RemoveCompanyPresentationVideoCommand(companyId),
        cancellationToken: cancellationToken
    );

    [HttpPatch("{companyId:guid}/galery")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateGaleryAsync(
        Guid companyId,
        IFormFile file,
        CancellationToken cancellationToken
    )
    {
        if (file is not { Length: > 0 })
        {
            return BadRequest(Error.BadRequest("No file uploaded."));
        }

        if (file.ContentType is not MediaTypeNames.Image.Jpeg and not MediaTypeNames.Image.Png)
        {
            return BadRequest(Error.BadRequest("Invalid file type."));
        }

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken);

        return await HandleAsync(
            new UpdateCompanyGaleryCommand(companyId, stream, file.ContentType),
            cancellationToken: cancellationToken
        );
    }

    [HttpDelete("{companyId:guid}/galery")]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> RemoveGaleryItemAsync(
        Guid companyId,
        [FromBody] string url,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new RemoveCompanyGaleryItemCommand(companyId, url),
        cancellationToken: cancellationToken
    );
}
