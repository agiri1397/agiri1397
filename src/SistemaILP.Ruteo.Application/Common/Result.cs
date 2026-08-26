namespace SistemaILP.Ruteo.Application.Common;

/// <summary>
/// Simple operation-result wrapper so services can return success/failure
/// without throwing exceptions for expected, user-facing error cases
/// (invalid credentials, validation errors, network failures, etc.).
/// Title/MessageType son metadata opcional para que la UI pueda mostrar el
/// mensaje con el sistema centralizado (icono + titulo + texto) sin tener
/// que adivinar el tipo a partir del texto. Los parametros son opcionales
/// y con default (null / MessageType.Error) para no romper ningun llamado
/// existente a Result.Failure(...).
/// </summary>
public class Result
{
    public bool Succeeded { get; }

    public string? Error { get; }

    public string? ErrorTitle { get; }

    public MessageType MessageType { get; }

    protected Result(bool succeeded, string? error, string? errorTitle, MessageType messageType)
    {
        Succeeded = succeeded;
        Error = error;
        ErrorTitle = errorTitle;
        MessageType = messageType;
    }

    public static Result Success() => new(true, null, null, MessageType.Success);

    public static Result Failure(string error, string? title = null, MessageType type = MessageType.Error) =>
        new(false, error, title, type);
}

public class Result<T> : Result
{
    public T? Data { get; }

    protected Result(bool succeeded, T? data, string? error, string? errorTitle, MessageType messageType)
        : base(succeeded, error, errorTitle, messageType)
    {
        Data = data;
    }

    public static Result<T> Success(T data) => new(true, data, null, null, MessageType.Success);

    public static new Result<T> Failure(string error, string? title = null, MessageType type = MessageType.Error) =>
        new(false, default, error, title, type);
}
