using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Scenes.ValueObjects;

public static class EffectTypeNameErrors
{
    private const string _baseErrorCode = "effect_type.name";
    public const string EmptyErrorCode = $"{_baseErrorCode}.empty";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";

    public static Error Empty => new Error("Effect type name cannot be empty").Validation(EmptyErrorCode);
    public static Error InvalidLength(int max) => new Error($"Effect type name cannot exceed {max} characters").Validation(InvalidLengthErrorCode);
}
