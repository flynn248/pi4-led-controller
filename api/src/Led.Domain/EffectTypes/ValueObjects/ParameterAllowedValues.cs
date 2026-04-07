using System.Text.Json;
using System.Text.RegularExpressions;
using FluentResults;

namespace Led.Domain.EffectTypes.ValueObjects;

public sealed partial record ParameterAllowedValues
{
    private ParameterAllowedValues(string? value) => Value = value;
    public string? Value { get; init; }
    public const int MaxLength = 1280;

    public static ParameterAllowedValues Empty => new((string?)null);

    public static Result<ParameterAllowedValues> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Empty;
        }

        value = value.Trim();

        if (!IsValidJson(value))
        {
            return Result.Fail<ParameterAllowedValues>(ParameterAllowedValuesErrors.InvalidFormat);
        }

        if (IsEmptyJson().IsMatch(value))
        {
            return Empty;
        }

        if (value.Length > MaxLength)
        {
            return Result.Fail<ParameterAllowedValues>(ParameterAllowedValuesErrors.InvalidLength(MaxLength));
        }

        return new ParameterAllowedValues(value);
    }

    private static bool IsValidJson(string value)
    {
        try
        {
            JsonDocument.Parse(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    [GeneratedRegex(@"^[\s{}\[\]]+$")]
    private static partial Regex IsEmptyJson();
}
