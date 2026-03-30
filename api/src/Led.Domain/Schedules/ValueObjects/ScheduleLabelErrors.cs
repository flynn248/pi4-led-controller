using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Schedules.ValueObjects;

public static class ScheduleLabelErrors
{
    private const string _baseErrorCode = "schedule.label";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";

    public static Error InvalidLength(int max) => new Error($"Label cannot exceed {max} characters").Validation(InvalidLengthErrorCode);
}
