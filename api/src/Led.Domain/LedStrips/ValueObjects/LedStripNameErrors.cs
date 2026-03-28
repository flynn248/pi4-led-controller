using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.LedStrips.ValueObjects;

public static class LedStripNameErrors
{
    private const string _baseErrorCode = "name";
    public const string EmptyErrorCode = $"{_baseErrorCode}.empty";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";

    public static Error Empty => new Error("Name cannot be empty").Validation(EmptyErrorCode);
    public static Error InvalidLength(int length) => new Error($"Name cannot exceed {length} characters").Validation(InvalidLengthErrorCode);
}
