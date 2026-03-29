using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Devices.ValueObjects;

public static class SerialNumberErrors
{
    private const string _baseErrorCode = "device.serial_number";
    public const string InvalidErrorCode = $"{_baseErrorCode}.invalid";

    public static Error Invalid => new Error("Device serial number is invalid").Validation(InvalidErrorCode);
}
