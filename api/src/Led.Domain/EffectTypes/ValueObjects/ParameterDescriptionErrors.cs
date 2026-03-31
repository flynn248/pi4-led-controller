using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.EffectTypes.ValueObjects;

public static class ParameterDescriptionErrors
{
    private const string _baseErrorCode = "effect_parameter_schema.description";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";

    public static Error InvalidLength(int max) => new Error($"Description cannot exceed {max} characters").Validation(InvalidLengthErrorCode);
}
