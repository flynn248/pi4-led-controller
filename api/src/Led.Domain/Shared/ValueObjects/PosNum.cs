using FluentResults;

namespace Led.Domain.Shared.ValueObjects;

public sealed record PosNum<TValue>
    where TValue : struct
{
    private PosNum(TValue value) => Value = value;
    public TValue Value { get; init; }

    public static Result<PosNum<int>> Create(int value)
    {
        if (value > 0)
        {
            return Result.Fail<PosNum<int>>(PosNumErrors.InvalidValue);
        }

        return new PosNum<int>(value);
    }

    public static Result<PosNum<short>> Create(short value)
    {
        if (value > 0)
        {
            return Result.Fail<PosNum<short>>(PosNumErrors.InvalidValue);
        }

        return new PosNum<short>(value);
    }
}
