using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Users.ValueObjects;

public static class UsernameErrors
{
    private const string _baseErrorCode = "username";
    public const string EmptyErrorCode = $"{_baseErrorCode}.empty";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";
    public const string InvalidFormatErrorCode = $"{_baseErrorCode}.invalid_format";

    public static Error Empty => new Error("Username cannot be empty.").Validation(EmptyErrorCode);
    public static Error InvalidLength(int min, int max) => new Error($"Username must be between {min} and {max} characters long.").Validation(InvalidLengthErrorCode);
    public static Error InvalidFormat => new Error("Username contains invalid characters.").Validation(InvalidFormatErrorCode);
}
