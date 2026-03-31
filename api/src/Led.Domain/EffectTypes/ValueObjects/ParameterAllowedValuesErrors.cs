using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Scenes.ValueObjects;

public static class ParameterAllowedValuesErrors
{
    private const string _baseErrorCode = "effect_parameter_schema.allowed_values";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";
    public const string InvalidFormatErrorCode = $"{_baseErrorCode}.invalid_format";

    public static Error InvalidLength(int max) => new Error($"Schema cannot exceed {max} characters").Validation(InvalidLengthErrorCode);
    public static Error InvalidFormat => new Error("Schema is in an invalid JSON format").Validation(InvalidFormatErrorCode);
}
