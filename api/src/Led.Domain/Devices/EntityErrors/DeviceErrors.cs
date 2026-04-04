using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Devices.EntityErrors;

public static class DeviceErrors
{
    private const string _baseErrorCode = "device";
    public const string IPAddressExistsErrorCode = $"{_baseErrorCode}.ip_address.exists";
    public const string SerialNumberExistsErrorCode = $"{_baseErrorCode}.serial_number.exists";

    public static Error IPAddressExists => new Error("IP address already in use").Validation(IPAddressExistsErrorCode);
    public static Error SerialNumerExists => new Error("Serial number already in use").Validation(SerialNumberExistsErrorCode);
}
