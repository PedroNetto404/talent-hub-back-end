namespace TalentHub.ApplicationCore.Core.Results;

public static class ResultExtensions
{
    public static async Task<TOut> FailIfNullAsync<TIn, TOut>(
        this Task<Result<TIn>> resultTask,
        Func<TIn, TOut> onSuccess,
        Func<Error, TOut> onError
    )
        where TIn : notnull
        where TOut : notnull
    {
        Result<TIn> result = await resultTask;

        return result.IsOk
            ? onSuccess(result.Value)
            : onError(result.Error);
    }

    public static async Task<TOut> FailIfNullAsync<TOut>(
        this Task<Result> resultTask,
        Func<TOut> onSuccess,
        Func<Error, TOut> onError
    )
    {
        Result result = await resultTask;

        return result.IsOk
            ? onSuccess()
            : onError(result.Error);
    }

    public static async Task<Result<T>> FailIfNullAsync<T>(
        this Task<T?> maybeTTask,
        Func<Error> errFactory) where T : notnull =>
        await maybeTTask is { } value
            ? Result.Ok<T>(value)
            : errFactory();
}
