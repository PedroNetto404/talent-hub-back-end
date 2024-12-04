namespace TalentHub.ApplicationCore.Core.Results;

public class Result
{
    private readonly Error? _error;

    protected Result(Error error) => _error = error;
    protected Result() => _error = null;

    public Error Error =>
        IsFail
        ? _error!
        : throw new InvalidOperationException("Successfully result cannot hava error");

    public bool IsFail => _error is not null;
    public bool IsOk => !IsFail;

    public static Result Ok() => new();
    public static Result<T> Ok<T>(T value) where T : notnull => new(value);
    public static Result Fail(Error error) => new(error);
    public static Result<T> Fail<T>(Error error) where T : notnull => new(error);

    public static implicit operator Result(Error error) => new(error);

    public static Result FailEarly(params Func<Result>[] funcs)
    {
        foreach (Func<Result> func in funcs)
        {
            if (func() is { IsFail: true } result)
            {
                return result;
            }
        }

        return Ok();
    }

    public static Result FailIf(bool condition, string erroMessage) =>
        condition 
        ? Fail(Error.BadRequest(erroMessage)) 
        : Ok();

    public static Result FailIfNull(object? value, string errorMessage) =>
        FailIf(value is null, errorMessage);

    public static Result FailIfIsInPast(DateTime value, string errorMessage) =>
        FailIf(value < DateTime.Now, errorMessage);

    public static Result FailIfIsInPast(DateOnly value, string errorMessage) =>
        FailIf(value < DateOnly.FromDateTime(DateTime.Now), errorMessage);

    public static Result FailIf(Func<bool> predicate, string erroMessage) =>
        FailIf(predicate(), erroMessage);

    public static Result FailIfIsNullOrWhiteSpace(string value, string errorMessage) =>
        FailIf(string.IsNullOrWhiteSpace(value), errorMessage);

    public static Result IsGreaterThan(decimal value, decimal limit, string errorMessage) =>
        FailIf(value <= limit, errorMessage);

    public static Result IsGreaterOrEqualThan(decimal value, decimal limit, string errorMessage) =>
        FailIf(value < limit, errorMessage);
    
    public static Result IsLessThan(decimal value, decimal limit, string errorMessage) =>
        FailIf(value >= limit, errorMessage);

    public static Result IsLessOrEqualThan(decimal value, decimal limit, string errorMessage) =>
        FailIf(value > limit, errorMessage);

    public static Result IsBetween(decimal value, decimal min, decimal max, string errorMessage) =>
        FailIf(value < min || value > max, errorMessage);

    public static Result IsBetweenOrEqual(decimal value, decimal min, decimal max, string errorMessage) =>
        FailIf(value <= min || value >= max, errorMessage);
}

public class Result<T> : Result where T : notnull
{
    private readonly T? _value;

    protected internal Result(T value) => _value = value;
    protected internal Result(Error error) : base(error) { }

    public T Value =>
        IsOk
        ? _value!
        : throw new InvalidOperationException("Cannot get value from fail result.");

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Error error) => new(error);
    public static implicit operator T(Result<T> result) => result.Value;
}


