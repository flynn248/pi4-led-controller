using System.Net;
using FluentResults;

namespace Led.Domain.Devices.ValueObjects;

public sealed record DeviceIpAddress
{
    private DeviceIpAddress(string value) => Value = value;
    public string Value { get; init; }
    // TODO: Max length?

    public static Result<DeviceIpAddress> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<DeviceIpAddress>(DeviceIpAddressErrors.Invalid);
        }

        value = value.Trim();

        if (!IPAddress.TryParse(value, out var _))
        {
            return Result.Fail<DeviceIpAddress>(DeviceIpAddressErrors.Invalid);
        }

        return new DeviceIpAddress(value);
    }
}
