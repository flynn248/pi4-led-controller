using FluentResults;

namespace Led.Domain.EffectTypes.ValueObjects;

public sealed record ParameterDescription
{
    private ParameterDescription(string value) => Value = value;
    public string Value { get; init; }
    public const int MaxLength = 200;

    public static Result<ParameterDescription> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new ParameterDescription(string.Empty);
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<ParameterDescription>(ParameterDescriptionErrors.InvalidLength(MaxLength));
        }

        return new ParameterDescription(value);
    }
}
