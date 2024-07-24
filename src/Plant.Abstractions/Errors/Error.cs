using Microsoft.AspNetCore.Http;

namespace Plant.Errors;

public class Error
{
    /// <summary>
    /// Gets the unique error code.
    /// </summary>
    public string Code { get; } = null!;

    /// <summary>
    /// Gets the error description.
    /// </summary>
    public string Description { get; } = null!;

    /// <summary>
    /// Gets the error type.
    /// </summary>
    public ErrorType ErrorType { get; }

    /// <summary>
    /// Gets the error http status code.
    /// </summary>
    public int StatusCode { get; }

    public Error(string code, string description, ErrorType errorType)
    {
        Code = code;
        Description = description;
        ErrorType = errorType;
        StatusCode = errorType switch
        {
            ErrorType.Failure => StatusCodes.Status400BadRequest,
            ErrorType.Unexpected => StatusCodes.Status400BadRequest,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    /// <summary>
    /// Creates an <see cref="Error"/> of type <see cref="ErrorType.Failure"/> from a code and description.
    /// </summary>
    /// <param name="code">The unique error code.</param>
    /// <param name="description">The error description.</param>
    public static Error Failure(
        string code = "General.Failure",
        string description = "A failure has occurred.") =>
        new(code, description, ErrorType.Failure);

    /// <summary>
    /// Creates an <see cref="Error"/> of type <see cref="ErrorType.Unexpected"/> from a code and description.
    /// </summary>
    /// <param name="code">The unique error code.</param>
    /// <param name="description">The error description.</param>
    public static Error Unexpected(
        string code = "General.Unexpected",
        string description = "An unexpected error has occurred.") =>
        new(code, description, ErrorType.Unexpected);

    /// <summary>
    /// Creates an <see cref="Error"/> of type <see cref="ErrorType.Validation"/> from a code and description.
    /// </summary>
    /// <param name="code">The unique error code.</param>
    /// <param name="description">The error description.</param>
    public static Error Validation(
        string code = "General.Validation",
        string description = "A validation error has occurred.") =>
        new(code, description, ErrorType.Validation);

    /// <summary>
    /// Creates an <see cref="Error"/> of type <see cref="ErrorType.Conflict"/> from a code and description.
    /// </summary>
    /// <param name="code">The unique error code.</param>
    /// <param name="description">The error description.</param>
    public static Error Forbidden(
        string code = "General.Forbidden",
        string description = "A 'Forbidden' error has occurred.") =>
        new(code, description, ErrorType.Forbidden);

    /// <summary>
    /// Creates an <see cref="Error"/> of type <see cref="ErrorType.Conflict"/> from a code and description.
    /// </summary>
    /// <param name="code">The unique error code.</param>
    /// <param name="description">The error description.</param>
    public static Error Conflict(
        string code = "General.Conflict",
        string description = "A conflict error has occurred.") =>
        new(code, description, ErrorType.Conflict);

    /// <summary>
    /// Creates an <see cref="Error"/> of type <see cref="ErrorType.NotFound"/> from a code and description.
    /// </summary>
    /// <param name="code">The unique error code.</param>
    /// <param name="description">The error description.</param>
    public static Error NotFound(
        string code = "General.NotFound",
        string description = "A 'Not Found' error has occurred.") =>
        new(code, description, ErrorType.NotFound);

    public override string ToString()
    {
        return $"{Code}: {Description}";
    }
}
