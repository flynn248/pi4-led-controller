using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Devices.ValueObjects;

public static class HostnameErrors
{
    private const string _baseErrorCode = "hostname";
    public const string EmptyErrorCode = $"{_baseErrorCode}.empty";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";

    public static Error Empty => new Error("Hostname cannot be empty").Validation(EmptyErrorCode);
    public static Error InvalidLength(int length) => new Error($"Hostname cannot exceed {length} characters").Validation(InvalidLengthErrorCode);

}
