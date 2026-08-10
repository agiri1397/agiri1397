namespace MauiBlazorCleanArchitecture.Application.Common;

/// <summary>
/// Simple operation-result wrapper so services can return success/failure
/// without throwing exceptions for expected, user-facing error cases
/// (invalid credentials, validation errors, network failures, etc.).
/// </summary>
public class Result
{
    public bool Succeeded { get; }

    public string? Error { get; }

    protected Result(bool succeeded, string? error)
    {
        Succeeded = succeeded;
        Error = error;
    }

    public static Result Success() => new(true, null);

    public static Result Failure(string error) => new(false, error);
}

public class Result<T> : Result
{
    public T? Data { get; }

    protected Result(bool succeeded, T? data, string? error) : base(succeeded, error)
    {
        Data = data;
    }

    public static Result<T> Success(T data) => new(true, data, null);

    public static new Result<T> Failure(string error) => new(false, default, error);
}
