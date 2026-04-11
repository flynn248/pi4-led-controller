using System.Text.Json;
using FluentResults;
using Led.SharedKernal.Extensions;

namespace Led.Domain.Scenes.ValueObjects;

public sealed record EffectParameter
{
    private EffectParameter(string? value) => Value = value;
    public string? Value { get; init; }
    public const int MaxLength = 1280;

    public static EffectParameter Empty => new((string?)null);

    public static Result<EffectParameter> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Empty;
        }

        value = value.Trim();

        if (!IsValidJson(value))
        {
            return Result.Fail<EffectParameter>(EffectParameterErrors.InvalidFormat);
        }

        if (value.IsEmptyJson())
        {
            return Empty;
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
            JsonDocument.Parse(value);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
