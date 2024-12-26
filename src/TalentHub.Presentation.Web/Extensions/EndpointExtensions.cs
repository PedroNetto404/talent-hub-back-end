using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.Presentation.Web.Extensions;

public static class EndpointExtensions
{
    public static async Task HandleUseCaseAsync<TReq, TUseCase>(
        this Ep.Req<TReq>.NoRes endpoint,
        TUseCase useCase,
        CancellationToken ct,
        Func<IResult>? onSuccessCallback = null
    )
        where TReq : notnull
        where TUseCase : IUseCaseInput
    {
        Result useCaseResult = await endpoint
            .Resolve<ISender>()
            .Send(useCase, ct);

        onSuccessCallback ??= () => Results.Ok();
        await SendResultAsync(
            useCaseResult, 
            endpoint.HttpContext, 
            onSuccessCallback);
    }

    public static async Task HandleUseCaseAsync<TReq, TRes, TUseCase>(
        this Ep.Req<TReq>.Res<TRes> endpoint,
        TUseCase useCase,
        CancellationToken ct,
        Func<TRes, IResult>? onSuccessCallback = null
    )
        where TReq : notnull
        where TRes : notnull
        where TUseCase : IUseCaseInput<TRes>
    {
        Result<TRes> useCaseResult = await endpoint
            .Resolve<ISender>()
            .Send(useCase, ct);

        onSuccessCallback ??= res => Results.Json(res, statusCode: StatusCodes.Status200OK);
        IResult onSuccessCallbackWrapper() => onSuccessCallback(useCaseResult.Value);

        await SendResultAsync(
            useCaseResult, 
            endpoint.HttpContext,
onSuccessCallbackWrapper);
    }

    private static async Task SendResultAsync(
        Result result,
        HttpContext context,
        Func<IResult> onSuccessCallback
    )
    {
        if(result is { IsFail: true, Error: var error})
        {
            IResult httpResult = error.Code switch
            {
                Error.NotFoundCode => Results.NotFound(error),
                Error.InvalidInputCode => Results.BadRequest(error),
                Error.UnauthorizedCode => Results.Unauthorized(),
                _ => Results.Json(error, statusCode: StatusCodes.Status500InternalServerError),
            };

            await context.Response.SendResultAsync(httpResult);
            return;
        }

        await context.Response.SendResultAsync(onSuccessCallback());
    }
}
