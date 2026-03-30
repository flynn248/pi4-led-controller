using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Scenes.ValueObjects;

public static class LedColorErrors
{
    private const string _baseErrorCode = "led.color";
    public const string InvalidValueErrorCode = $"{_baseErrorCode}.invalid_value";

    public static Error InvalidValue(short min, short max) => new Error($"Color value must be between {min} and {max}").Validation(InvalidValueErrorCode);
}
