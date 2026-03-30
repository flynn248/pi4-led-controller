using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Schedules.ValueObjects;

public static class CronExpressionErrors
{
    private const string _baseErrorCode = "schedule.cron_expression";
    public const string InvalidErrorCode = $"{_baseErrorCode}.invalid";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";

    public static Error Invalid => new Error("Cron expression is invalid").Validation(InvalidErrorCode);
    public static Error InvalidLength(int max) => new Error($"Cron expression cannot exceed {max} characters").Validation(InvalidLengthErrorCode);
}
