using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Devices.ValueObjects;

public static class DeviceIpAddressErrors
{
    private const string _baseErrorCode = "device.ip_address";
    public const string InvalidErrorCode = $"{_baseErrorCode}.invalid";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";

    public static Error Invalid => new Error("IP address is invalid").Validation(InvalidErrorCode);
    public static Error InvalidLength(int max) => new Error($"IP address cannot exceed {max} characters").Validation(InvalidLengthErrorCode);
}
