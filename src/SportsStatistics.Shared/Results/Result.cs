namespace SportsStatistics.Shared.Results;

/// <summary>
/// Represents the outcome of an operation, including success or failure and associated result error.
/// </summary>
public class Result
{
    protected internal Result(bool isSuccess, ResultError error)
    {
        if (isSuccess && error != ResultError.None)
        {
            throw new InvalidOperationException("Result cannot be both successful and have an error.");
        }

        if (!isSuccess && error == ResultError.None)
        {
            throw new InvalidOperationException("Result cannot be unsuccessful but not have an error.");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public ResultError Error { get; }

    public static Result Success() => new(true, ResultError.None);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, ResultError.None);

    public static Result Failure(ResultError error) => new(false, error);

    public static Result<TValue> Failure<TValue>(ResultError error) => new(default, false, error);

    public static Result<TValue> Create<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(ResultError.NullValue);
}
