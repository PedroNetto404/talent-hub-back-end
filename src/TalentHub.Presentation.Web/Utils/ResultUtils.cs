using System;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.Presentation.Web.Utils;

public static class ResultUtils
{
    public static IResult Map(Result result, Func<IResult>? onSuccess = null)
    {
        if (result is { IsFail: true, Error: var error })
        {
            return error.Code switch
            {
                "bad_request" => Results.BadRequest(error),
                "not_found" => Results.NotFound(error),
                "unauthorized" => Results.Unauthorized(),
                _ => Results.Json(error, statusCode: StatusCodes.Status500InternalServerError)
            };
        }

        onSuccess ??= () => Results.Ok();
        return onSuccess();
    }
    public static IResult Map<TResponse>(
        Result<TResponse> result, 
        Func<TResponse, IResult>? onSuccess = null
    ) where TResponse : notnull 
    {
        Func<IResult> callback = 
            onSuccess is not null 
            ? () => onSuccess(result.Value) 
            : () => Results.Ok(result.Value);

        return Map(result, callback);
    }    
}
