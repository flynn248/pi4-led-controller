using FluentResults;

namespace Led.Domain.EffectTypes.ValueObjects;

public sealed record EffectTypeDescription
{
    private EffectTypeDescription(string value) => Value = value;
    public string Value { get; init; }
    public const int MaxLength = 200;
    public static EffectTypeDescription Empty => new(string.Empty);

    public static Result<EffectTypeDescription> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Empty;
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<EffectTypeDescription>(EffectTypeDescriptionErrors.InvalidLength(MaxLength));
        }

        return new EffectTypeDescription(value);
    }
}
