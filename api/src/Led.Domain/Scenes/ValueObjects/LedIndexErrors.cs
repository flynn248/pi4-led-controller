using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Scenes.ValueObjects;

public static class LedIndexErrors
{
    private const string _baseErrorCode = "led.index";
    public const string InvalidValueErrorCode = $"{_baseErrorCode}.invalid_value";

    public static Error InvalidValue(short min) => new Error($"Led index cannot be less than {min}").Validation(InvalidValueErrorCode);
}
