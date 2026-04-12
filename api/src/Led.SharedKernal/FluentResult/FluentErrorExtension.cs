using FluentResults;
using Led.SharedKernal.Errors;

namespace Led.SharedKernal.FluentResult;

public static class FluentErrorExtension
{
    public const string ErrorCodeKey = "errorCode";
    public const string ErrorTypeKey = "errorType";

    /// <summary>
    /// Creates a new instance of <typeparamref name="TError"/>  with the specified code and description, representing a system error.
    /// </summary>
    /// <typeparam name="TError"></typeparam>
    /// <param name="error">The error instance.</param>
    /// <param name="code">The error code.</param>
    /// <returns>A new instance of <typeparamref name="TError"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="error"/> is null.</exception>
    public static TError Failure<TError>(this TError error, string code)
        where TError : Error
    {
        ArgumentNullException.ThrowIfNull(error);

        return (TError)error.WithMetadata(ErrorCodeKey, code)
            .WithMetadata(ErrorTypeKey, ErrorType.Failure);
    }

    /// <summary>
    /// Creates a new instance of <typeparamref name="TError"/>  with the specified code and description, representing a not found error.
    /// </summary>
    /// <typeparam name="TError"></typeparam>
    /// <param name="error">The error instance.</param>
    /// <param name="code">The error code.</param>
    /// <returns>A new instance of <typeparamref name="TError"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="error"/> is null.</exception>
    public static TError NotFound<TError>(this TError error, string code)
        where TError : Error
    {
        ArgumentNullException.ThrowIfNull(error);

        return (TError)error.WithMetadata(ErrorCodeKey, code)
            .WithMetadata(ErrorTypeKey, ErrorType.NotFound);
    }

    /// <summary>
    /// Creates a new instance of <typeparamref name="TError"/>  with the specified code and description, representing a validation error.
    /// </summary>
    /// <typeparam name="TError"></typeparam>
    /// <param name="error">The error instance.</param>
    /// <param name="code">The error code.</param>
    /// <returns>A new instance of <typeparamref name="TError"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="error"/> is null.</exception>
    public static TError Validation<TError>(this TError error, string code)
        where TError : Error
    {
        ArgumentNullException.ThrowIfNull(error);

        return (TError)error.WithMetadata(ErrorCodeKey, code)
            .WithMetadata(ErrorTypeKey, ErrorType.Validation);
    }

    /// <summary>
    /// Creates a new instance of <typeparamref name="TError"/>  with the specified code and description, representing a conflict error.
    /// </summary>
    /// <typeparam name="TError"></typeparam>
    /// <param name="error">The error instance.</param>
    /// <param name="code">The error code.</param>
    /// <returns>A new instance of <typeparamref name="TError"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="error"/> is null.</exception>
    public static TError Conflict<TError>(this TError error, string code)
        where TError : Error
    {
        ArgumentNullException.ThrowIfNull(error);

        return (TError)error.WithMetadata(ErrorCodeKey, code)
            .WithMetadata(ErrorTypeKey, ErrorType.Conflict);
    }

    /// <summary>
    /// Creates a new instance of <typeparamref name="TError"/>  with the specified code and description, representing an access unauthorized error.
    /// </summary>
    /// <typeparam name="TError"></typeparam>
    /// <param name="error">The error instance.</param>
    /// <param name="code">The error code.</param>
    /// <returns>A new instance of <typeparamref name="TError"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="error"/> is null.</exception>
    public static TError AccessUnauthorized<TError>(this TError error, string code)
        where TError : Error
    {
        ArgumentNullException.ThrowIfNull(error);

        return (TError)error.WithMetadata(ErrorCodeKey, code)
            .WithMetadata(ErrorTypeKey, ErrorType.AccessUnauthorized);
    }

    /// <summary>
    /// Creates a new instance of <typeparamref name="TError"/>  with the specified code and description, representing an access forbidden error.
    /// </summary>
    /// <typeparam name="TError"></typeparam>
    /// <param name="error">The error instance.</param>
    /// <param name="code">The error code.</param>
    /// <returns>A new instance of <typeparamref name="TError"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="error"/> is null.</exception>
    public static TError AccessForbidden<TError>(this TError error, string code)
        where TError : Error
    {
        ArgumentNullException.ThrowIfNull(error);

        return (TError)error.WithMetadata(ErrorCodeKey, code)
            .WithMetadata(ErrorTypeKey, ErrorType.AccessForbidden);
    }

    /// <summary>
    /// Returns the <see cref="ErrorType"/> of the specified <paramref name="error"/> instance. If the error does not have an associated <see cref="ErrorType"/>, it returns <see cref="ErrorType.None"/>.
    /// </summary>
    /// <param name="error">The error instance.</param>
    /// <returns>The <see cref="ErrorType"/></returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="error"/> is null.</exception>
    public static ErrorType GetErrorType(this IError error)
    {
        ArgumentNullException.ThrowIfNull(error);

        if (!error.HasMetadataKey(ErrorTypeKey))
        {
            return ErrorType.None;
        }

        if (error.Metadata.TryGetValue(ErrorTypeKey, out object type))
        {
            return (ErrorType)type;
        }

        return ErrorType.None;
    }

    /// <summary>
    /// Retrieves the error code associated with the specified error, if available.
    /// </summary>
    /// <remarks>If the <see cref="Error"/> does not contain an error code in its metadata, this method returns an empty string. This method does not throw an exception if the error code is missing.</remarks>
    /// <param name="error">The <see cref="Error"/> instance from which to retrieve the error code. Cannot be null.</param>
    /// <returns>A string containing the error code if present; otherwise, an empty string.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="error"/> is null.</exception>
    public static string GetErrorCode(this IError error)
    {
        ArgumentNullException.ThrowIfNull(error);

        if (!error.HasMetadataKey(ErrorCodeKey))
        {
            return string.Empty;
        }

        if (error.Metadata.TryGetValue(ErrorCodeKey, out object code))
        {
            return code?.ToString() ?? string.Empty;
        }

        return string.Empty;
    }
}
