using System.Reflection.Metadata;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.Presentation.Web.Controllers;

[ApiController]
public class ApiController(ISender sender) : ControllerBase
{
    protected async Task<IActionResult> HandleAsync<TResult>(
        IUseCaseInput<TResult> input,
        Func<TResult, IActionResult>? onSuccess = null,
        CancellationToken cancellationToken = default) where TResult : notnull
    {
        Result<TResult> result = await sender.Send(input, cancellationToken);

        if (result is { IsFail: true, Error: var err })
        {
            return err is NotFoundError
            ? NotFound(err)
            : BadRequest(err);
        }

        onSuccess ??= (_) => Ok(result.Value);
        return onSuccess(result.Value);
    }

    protected async Task<IActionResult> HandleAsync(
        IUseCaseInput input,
        Func<IActionResult>? onSuccess = null,
        CancellationToken cancellationToken = default
    )
    {
        Result result = await sender.Send(input, cancellationToken);

        if (result is { IsFail: true, Error: var err })
        {
            return err is NotFoundError
           ? NotFound(err)
           : BadRequest(err);
        }

        onSuccess ??= Ok;
        return onSuccess();
    }
}
