using FluentResults;

namespace Led.Domain.EffectTypes.ValueObjects;

public sealed record ParameterKey
{
    private ParameterKey(string value) => Value = value;
    public string Value { get; init; }
    public const int MaxLength = 60;

    public static Result<ParameterKey> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<ParameterKey>(ParameterKeyErrors.Empty);
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<ParameterKey>(ParameterKeyErrors.InvalidLength(MaxLength));
        }

        return new ParameterKey(value);
    }
}
