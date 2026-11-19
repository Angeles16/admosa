namespace admosa.Domain.Errors;

public class AppException : Exception
{
    public ErrorType Type { get; private set; }
    public string? Code { get; private set; }

    public AppException(ErrorType errorType, string message, string? errorCode = null) : base(message)
    {
        Type = errorType;
        Code = errorCode;
    }

    public static AppException Validation(string code, string message)   => new(ErrorType.Validation, code, message);
    public static AppException Unauthorized(string code, string message) => new(ErrorType.Unauthorized, code, message);
    public static AppException Forbidden(string code, string message)    => new(ErrorType.Forbidden, code, message);
    public static AppException NotFound(string code, string message)     => new(ErrorType.NotFound, code, message);
    public static AppException Conflict(string code, string message)     => new(ErrorType.Conflict, code, message);
}