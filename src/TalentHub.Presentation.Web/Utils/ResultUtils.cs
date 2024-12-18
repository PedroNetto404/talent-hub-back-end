using System;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.Presentation.Web.Utils;

public static class ResultUtils
{
    public static IResult MatchResult(Result result, Func<IResult>? onSuccess = null)
    {
        if (result is { IsFail: true, Error: var error })
        {
            return error switch
            {
                { Code: "bad_request", Expose: true } => Results.BadRequest(error),
                { Code: "not_found", Expose: true } => Results.NotFound(error),
                { Code: "unauthorized" } => Results.Unauthorized(),
                _ => Results.Json(error, statusCode: StatusCodes.Status500InternalServerError)
            };
        }

        onSuccess ??= () => Results.Ok();
        return onSuccess();
    }

    public static IResult MatchResult<TResponse>(
        Result<TResponse> result, 
        Func<TResponse, IResult>? onSuccess = null
    ) where TResponse : notnull 
    {
        Func<IResult> callback = 
            onSuccess is not null 
            ? () => onSuccess(result.Value) 
            : () => Results.Ok(result.Value);

        return MatchResult(result, callback);
    }    
}
