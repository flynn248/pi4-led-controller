using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Shared.ValueObjects;

public static class NumErrors
{
    private const string _baseErrorCode = "number";
    public const string InvalidValueErrorCode = $"{_baseErrorCode}.invalid_value";

    public static Error ZeroOrLess => new Error("Value must be > 0").Validation(InvalidValueErrorCode);
    public static Error LessThanZero => new Error("Value must be >= 0").Validation(InvalidValueErrorCode);
}
