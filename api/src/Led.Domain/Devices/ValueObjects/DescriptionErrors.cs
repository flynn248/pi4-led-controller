using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Devices.ValueObjects;

public static class DescriptionErrors
{
    private const string _baseErrorCode = "description";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";

    public static Error InvalidLength(int max) => new Error($"Description cannot exceed {max} characters").Validation(InvalidLengthErrorCode);
}
