namespace Led.Domain.Schedules.ValueObjects;

public enum ScheduleTypeId
{
    /// <summary>
    /// The schedule fires once at a specific time
    /// </summary>
    OneOff = 1,

    /// <summary>
    /// The schedule fires repeatedly based on a CRON expression
    /// </summary>
    Recurring
}
