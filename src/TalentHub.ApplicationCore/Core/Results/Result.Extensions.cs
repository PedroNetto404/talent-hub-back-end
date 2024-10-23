namespace TalentHub.ApplicationCore.Core.Results;

public static class ResultExtensions
{
    public static async Task<TOut> MatchAsync<TIn, TOut>(
        this Task<Result<TIn>> resultTask,
        Func<TIn, TOut> onSuccess,
        Func<Error, TOut> onError
    ) 
        where TIn : notnull 
        where TOut : notnull
    {
        var result = await resultTask;

        return result.IsOk 
        ? onSuccess(result.Value)
        : onError(result.Error);
    }

    public static async Task<TOut> MatchAsync<TOut>(
        this Task<Result> resultTask,
        Func<TOut> onSuccess,
        Func<Error, TOut> onError
    ) 
    {
        var result = await resultTask;

        return result.IsOk 
        ? onSuccess()
        : onError(result.Error);
    }
}