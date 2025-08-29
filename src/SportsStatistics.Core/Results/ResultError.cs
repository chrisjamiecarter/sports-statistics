namespace SportsStatistics.Core.Results;

/// <summary>
/// Represents aresult error with a code and message.
/// </summary>
public class ResultError : IEquatable<ResultError>
{
    public static readonly ResultError None = new(string.Empty, string.Empty);
    public static readonly ResultError NullValue = new("Error.NullValue", "The specified result value is null.");

    public ResultError(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }

    public string Message { get; }

    public static bool operator ==(ResultError? a, ResultError? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(ResultError? a, ResultError? b)
    {
        return !(a == b);
    }

    public virtual bool Equals(ResultError? other)
    {
        if (other is null)
        {
            return false;
        }

        return Code == other.Code && Message == other.Message;
    }

    public override bool Equals(object? obj)
    {
        return obj is ResultError error && Equals(error);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Message);
    }

    public override string ToString()
    {
        return Code;
    }
}
