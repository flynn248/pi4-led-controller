using FluentResults;

namespace Led.Domain.EffectTypes.ValueObjects;

public sealed record EffectTypeName
{
    private EffectTypeName(string value) => Value = value;
    public string Value { get; init; }
    public const int MaxLength = 80;

    public static Result<EffectTypeName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<EffectTypeName>(EffectTypeNameErrors.Empty);
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<EffectTypeName>(EffectTypeNameErrors.InvalidLength(MaxLength));
        }

        return new EffectTypeName(value);
    }
}
