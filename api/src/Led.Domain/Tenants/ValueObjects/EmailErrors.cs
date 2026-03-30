using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Tenants.ValueObjects;

public static class EmailErrors
{
    private const string _baseErrorCode = "email";
    public const string EmptyErrorCode = $"{_baseErrorCode}.empty";
    public const string InvalidFormatErrorCode = $"{_baseErrorCode}.invalid_format";
    public const string DuplicateErrorCode = $"{_baseErrorCode}.exists";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";

    public static Error Empty => new Error("Email cannot be empty").Validation(EmptyErrorCode);
    public static Error InvalidFormat => new Error("Invalid email format").Validation(InvalidFormatErrorCode);
    public static Error Duplicate => new Error("Provided email address already exists").Conflict(DuplicateErrorCode);
    public static Error InvalidLength(int max) => new Error($"Name cannot exceed {max} characters").Validation(InvalidLengthErrorCode);
}
