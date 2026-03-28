using FluentResults;

namespace Led.Domain.LedStrips.ValueObjects;

public sealed record LedStripName
{
    private LedStripName(string value) => Value = value;
    public string Value { get; init; }
    public const int MaxLength = 100;

    public static Result<LedStripName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<LedStripName>(LedStripNameErrors.Empty);
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<LedStripName>(LedStripNameErrors.InvalidLength(MaxLength));
        }

        return new LedStripName(value);
    }
}
