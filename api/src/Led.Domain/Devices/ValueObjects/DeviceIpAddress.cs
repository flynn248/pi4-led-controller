using System.Net;
using FluentResults;

namespace Led.Domain.Devices.ValueObjects;

public sealed record DeviceIpAddress
{
    private DeviceIpAddress(IPAddress value) => Value = value;
    public IPAddress Value { get; init; }
    public const int MaxLength = 15; // IPv4 is 15. Not supporting IPv6 for now

    private DeviceIpAddress()
    { }

    public static Result<DeviceIpAddress> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<DeviceIpAddress>(DeviceIpAddressErrors.Invalid);
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<DeviceIpAddress>(DeviceIpAddressErrors.InvalidLength(MaxLength));
        }

        if (!IPAddress.TryParse(value, out var ipAddr))
        {
            return Result.Fail<DeviceIpAddress>(DeviceIpAddressErrors.Invalid);
        }

        return new DeviceIpAddress(ipAddr);
    }
}
