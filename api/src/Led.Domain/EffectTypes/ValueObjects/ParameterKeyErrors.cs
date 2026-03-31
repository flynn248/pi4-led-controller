using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.EffectTypes.ValueObjects;

public static class ParameterKeyErrors
{
    private const string _baseErrorCode = "effect_parameter_schema.key";
    public const string EmptyErrorCode = $"{_baseErrorCode}.empty";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";

    public static Error Empty => new Error("Key cannot be empty").Validation(EmptyErrorCode);
    public static Error InvalidLength(int max) => new Error($"Key cannot exceed {max} characters").Validation(InvalidLengthErrorCode);
}
