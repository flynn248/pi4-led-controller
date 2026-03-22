using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.LedStrips.ValueObjects;

public static class PosNumErrors
{
    private const string _baseErrorCode = "number";
    public const string InvalidValueErrorCode = $"{_baseErrorCode}.invalid_value";

    public static Error InvalidValue => new Error("Value must be > 0.").Validation(InvalidValueErrorCode);
}
