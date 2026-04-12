using System.Numerics;
using FluentResults;

namespace Led.Domain.Shared.ValueObjects;

public sealed record PosNum<TValue>
    where TValue : INumber<TValue>
{
    private PosNum(TValue value) => Value = value;
    public TValue Value { get; init; }

    public static Result<PosNum<TValue>> Create(TValue value)
    {
        if (value <= TValue.Zero)
        {
            return Result.Fail(NumErrors.ZeroOrLess);
        }

        return new PosNum<TValue>(value);
    }

    public static implicit operator TValue(PosNum<TValue> posNum) => posNum.Value;
}
