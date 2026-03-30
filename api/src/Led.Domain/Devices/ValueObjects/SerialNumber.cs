using FluentResults;

namespace Led.Domain.Devices.ValueObjects;

public sealed record SerialNumber
{
    private SerialNumber(string value) => Value = value;
    public string Value { get; init; }
    public const int MaxLength = 100;

    public static Result<SerialNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<SerialNumber>(SerialNumberErrors.Invalid);
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<SerialNumber>(SerialNumberErrors.InvalidLength(MaxLength));
        }

        return new SerialNumber(value);
    }
}
