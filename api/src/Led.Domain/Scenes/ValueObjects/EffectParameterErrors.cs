using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Scenes.ValueObjects;

public static class EffectParameterErrors
{
    private const string _baseErrorCode = "effect_parameter";
    public const string EmptyErrorCode = $"{_baseErrorCode}.empty";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";
    public const string InvalidFormatErrorCode = $"{_baseErrorCode}.invalid_format";

    public static Error Empty => new Error("Effect parameter cannot be empty").Validation(EmptyErrorCode);
    public static Error InvalidLength(int max) => new Error($"Effect parameter cannot exceed {max} characters").Validation(InvalidLengthErrorCode);
    public static Error InvalidFormat => new Error("Effect parameter is in an invalid JSON format").Validation(InvalidFormatErrorCode);
}
