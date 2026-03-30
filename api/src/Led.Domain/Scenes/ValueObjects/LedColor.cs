using FluentResults;

namespace Led.Domain.Scenes.ValueObjects;

public sealed record LedColor
{
    private LedColor(short value) => Value = value;
    public short Value { get; init; }

    public const short MinValue = 0;
    public const short MaxValue = 255;

    public static Result<LedColor> Create(short value)
    {
        if (value < MinValue || value > MaxValue)
        {
            return Result.Fail<LedColor>(LedColorErrors.InvalidValue(MinValue, MaxValue));
        }

        return new LedColor(value);
    }
}
