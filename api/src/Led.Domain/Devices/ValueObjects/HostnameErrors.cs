using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Devices.ValueObjects;

public static class HostnameErrors
{
    private const string _baseErrorCode = "hostname";
    public const string EmptyErrorCode = $"{_baseErrorCode}.empty";

    public static Error Empty => new Error("Hostname cannot be empty.").Validation(EmptyErrorCode);
}
