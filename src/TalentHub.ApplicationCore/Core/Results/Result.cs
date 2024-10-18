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
}

public class Result<T> : Result where T : notnull
{
    private readonly T? _value;

    protected internal Result(T value) => _value = value;
    protected internal Result(Error error) : base(error) { }

    public new bool IsOk => base.IsOk && _value is not null; 

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Error error) => new(error);
}


