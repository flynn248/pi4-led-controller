using FluentResults;
using Led.SharedKernal.Errors;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Users.ValueObjects;

public static class EmailErrors
{
    private const string _baseErrorCode = "email";
    public const string EmptyErrorCode = $"{_baseErrorCode}.empty";
    public const string InvalidFormatErrorCode = $"{_baseErrorCode}.invalid_format";

    public static Error Empty => new Error("Email cannot be empty.").Validation(EmptyErrorCode);
    public static Error InvalidFormat => new Error("Invalid email format.").Validation(InvalidFormatErrorCode);
}
