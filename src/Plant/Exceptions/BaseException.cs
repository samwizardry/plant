using Microsoft.AspNetCore.Http;

namespace Plant.Exceptions;

public class BaseException : Exception
{
    public int StatusCode { get; init; } = StatusCodes.Status500InternalServerError;

    public string Code { get; init; } = null!;

    public string? DetailMessage { get; init; }

    public BaseException(
        int statusCode,
        string code,
        string? message,
        string? detailMessage,
        Exception? innerException = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        Code = code;
        DetailMessage = detailMessage;
    }
}

