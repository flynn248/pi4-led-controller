using System.Numerics;
using FluentResults;

namespace Led.Domain.Shared.ValueObjects;

public sealed record ZeroOrPosNum<TValue>
    where TValue : INumber<TValue>
{
    private ZeroOrPosNum(TValue value) => Value = value;
    public TValue Value { get; init; }

    public static ZeroOrPosNum<TValue> Empty => new(TValue.Zero);

    public static Result<ZeroOrPosNum<TValue>> Create(TValue value)
    {
        if (value < TValue.Zero)
        {
            return Result.Fail(NumErrors.LessThanZero);
        }

        return new ZeroOrPosNum<TValue>(value);
    }

    public static implicit operator TValue(ZeroOrPosNum<TValue> posNum) => posNum.Value;
}
