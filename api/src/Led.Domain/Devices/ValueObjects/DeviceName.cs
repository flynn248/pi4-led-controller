using FluentResults;

namespace Led.Domain.Devices.ValueObjects;

public sealed record DeviceName
{
    private DeviceName(string value) => Value = value;
    public string Value { get; init; }
    public const int MaxLength = 200;

    public static Result<DeviceName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<DeviceName>(DeviceNameErrors.Empty);
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<DeviceName>(DeviceNameErrors.InvalidLength(MaxLength));
        }

        return new DeviceName(value);
    }
}
