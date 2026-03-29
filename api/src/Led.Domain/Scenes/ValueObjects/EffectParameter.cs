using System.Text.Json;
using FluentResults;

namespace Led.Domain.Scenes.ValueObjects;

public sealed record EffectParameter
{
    private EffectParameter(string value) => Value = value;
    public string Value { get; init; }
    public const int MaxLength = 1280;

    public static Result<EffectParameter> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<EffectParameter>(EffectParameterErrors.Empty);
        }

        value = value.Trim();

        if (!IsValidJson(value))
        {
            return Result.Fail<EffectParameter>(EffectParameterErrors.InvalidFormat);
        }

        if (value.Length > MaxLength)
        {
            return Result.Fail<EffectParameter>(EffectParameterErrors.InvalidLength(MaxLength));
        }

        return new EffectParameter(value);
    }

    private static bool IsValidJson(string value)
    {
        try
        {
            if (value.StartsWith('{') && value.EndsWith('}'))
            {
                JsonDocument.Parse(value);
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}
