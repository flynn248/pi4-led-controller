using FluentResults;

namespace Led.Domain.Scenes.ValueObjects;

public sealed record LedIndex
{
    private LedIndex(short value) => Value = value;
    public short Value { get; init; }

    public const short MinValue = 0;

    public static Result<LedIndex> Create(short value)
    {
        if (value < MinValue)
        {
            return Result.Fail<LedIndex>(LedIndexErrors.InvalidValue(MinValue));
        }

        return new LedIndex(value);
    }
}
