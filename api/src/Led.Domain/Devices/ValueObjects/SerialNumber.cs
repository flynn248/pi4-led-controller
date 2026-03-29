using FluentResults;

namespace Led.Domain.Devices.ValueObjects;

public sealed record SerialNumber
{
    private SerialNumber(string value) => Value = value;
    public string Value { get; init; }
    // TODO: Max length?

    public static Result<SerialNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<SerialNumber>(SerialNumberErrors.Invalid);
        }

        value = value.Trim();

        return new SerialNumber(value);
    }
}
