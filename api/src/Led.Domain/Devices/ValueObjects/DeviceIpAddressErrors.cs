using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Devices.ValueObjects;

public static class DeviceIpAddressErrors
{
    private const string _baseErrorCode = "device.ip_address";
    public const string InvalidErrorCode = $"{_baseErrorCode}.invalid";

    public static Error Invalid => new Error("IP address is invalid").Validation(InvalidErrorCode);
}
