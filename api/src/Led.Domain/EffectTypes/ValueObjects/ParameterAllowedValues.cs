using System.Text.Json;
using FluentResults;

namespace Led.Domain.Scenes.ValueObjects;

public sealed record ParameterAllowedValues
{
    private ParameterAllowedValues(string? value) => Value = value;
    public string? Value { get; init; }
    public const int MaxLength = 1280;

    public static ParameterAllowedValues Empty => new((string?)null);

    public static Result<ParameterAllowedValues> Create(string value)
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
}
