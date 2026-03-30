using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Schedules.EntityErrors;

public static class ScheduleErrors
{
    private const string _baseErrorCode = "schedule";
    public const string InvalidScheduleTypeErrorCode = $"{_baseErrorCode}.invalid_type";
    public const string OneOffInvalidFormatErrorCode = $"{_baseErrorCode}.one_off.invalid_foramt";
    public const string RecurringInvalidFormatErrorCode = $"{_baseErrorCode}.recurring.invalid_foramt";

    public static Error InvalidScheduleType => new Error("Invalid schedule type").Validation(InvalidScheduleTypeErrorCode);
    public static Error OneOffMissingRunDate => new Error("The rundate needs to be specified").Validation(OneOffInvalidFormatErrorCode);
    public static Error OneOffWithCron => new Error("Cron expression cannot be defined for a one-off schedule type").Validation(OneOffInvalidFormatErrorCode);
    public static Error RecurringMissingCron => new Error("The cron expression needs to be defined").Validation(RecurringInvalidFormatErrorCode);
    public static Error RecurringWithRunDate => new Error("Rundate cannot be defined for a recurring schedule type").Validation(RecurringInvalidFormatErrorCode);
}
