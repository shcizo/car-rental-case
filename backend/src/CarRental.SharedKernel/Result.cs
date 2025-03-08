namespace CarRental.SharedKernel;

public enum ResultStatus
{
    Ok,
    Error,
    Forbidden,
    Unauthorized,
    Invalid,
    NotFound,
    NoContent,
    Conflict,
    CriticalError,
    Unavailable
}

/// <summary>
/// Represents a result of an operation with a return value.
/// </summary>
public class Result<T>
{
    public T? Value { get; }
    public ResultStatus Status { get; }
    public string Error { get; }
    private Result(T value)
    {
        Value = value;
        Error = string.Empty;
        Status = ResultStatus.Ok;
    }

    private Result(string error, ResultStatus status)
    {
        Value = default;
        Error = error;
        Status = status;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(string error) => new(error, ResultStatus.Error);
    public static Result<T> NotFound(string error) => new(error, ResultStatus.NotFound);
    public static Result<T> Invalid(string error) => new(error, ResultStatus.Invalid);
}

/// <summary>
/// Represents a result of an operation without a return value.
/// </summary>
public class Result
{
    public ResultStatus Status { get; }
    public string Error { get; }
    private Result(ResultStatus status, string? error = null)
    {
        Status = status;
        Error = error ?? string.Empty;
    }

    public static Result Success() => new(ResultStatus.Ok);
    public static Result Failure(string error) => new(ResultStatus.Error, error);
    public static Result NotFound(string error) => new(ResultStatus.NotFound, error);
    public static Result Invalid(string error) => new(ResultStatus.Invalid, error);
}