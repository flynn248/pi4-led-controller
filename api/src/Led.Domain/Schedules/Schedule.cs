using FluentResults;
using Led.Domain.Schedules.EntityErrors;
using Led.Domain.Schedules.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.Schedules;

public sealed class Schedule : AggregateRoot<Guid>
{
    public Guid TenantId { get; init; }
    public Guid SceneId { get; init; }
    public ScheduleLabel Label { get; private set; }
    public ScheduleTypeId ScheduleTypeId { get; private set; }
    public DateTime? RunAtUtc { get; private set; }
    public CronExpression CronExpression { get; private set; }
    public bool IsEnabled { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? ModifiedAtUtc { get; private set; }

    private Schedule()
    { }

    private Schedule(Guid id, Guid tenantId, Guid sceneId, ScheduleTypeId scheduleTypeId, bool isEnabled, ScheduleLabel label, DateTime? runAtUtc, CronExpression cronExpression, DateTime createdAtUtc)
        : base(id)
    {
        TenantId = tenantId;
        SceneId = sceneId;
        Label = label;
        ScheduleTypeId = scheduleTypeId;
        RunAtUtc = runAtUtc;
        CronExpression = cronExpression;
        IsEnabled = isEnabled;
        CreatedAtUtc = createdAtUtc;
    }

    public static Result<Schedule> Create(Guid tenantId, Guid sceneId, int scheduleTypeId, bool isEnabled, ScheduleLabel label, DateTime? runAtUtc, CronExpression cronExpression, DateTime createdAtUtc)
    {
        var validation = ValidateSchedule(scheduleTypeId, runAtUtc, cronExpression);

        if (validation.IsFailed)
        {
            return Result.Fail<Schedule>(validation.Errors);
        }

        return new Schedule(Guid.CreateVersion7(), tenantId, sceneId, (ScheduleTypeId)scheduleTypeId, isEnabled, label, runAtUtc, cronExpression, createdAtUtc);
    }

    public Result UpdateSchedule(int scheduleTypeId, DateTime? runAtUtc, CronExpression cronExpression, DateTime modifiedAtUtc)
    {
        var validation = ValidateSchedule(scheduleTypeId, runAtUtc, cronExpression);

        if (validation.IsFailed)
        {
            return validation;
        }

        ScheduleTypeId = (ScheduleTypeId)scheduleTypeId;
        RunAtUtc = runAtUtc;
        CronExpression = cronExpression;
        ModifiedAtUtc = modifiedAtUtc;

        // TODO: Domain event?

        return Result.Ok();
    }

    public Result Enable(DateTime modifiedAtUtc)
    {
        if (IsEnabled)
        {
            return Result.Ok();
        }

        IsEnabled = true;
        ModifiedAtUtc = modifiedAtUtc;

        return Result.Ok();
    }

    public Result Disable(DateTime modifiedAtUtc)
    {
        if (!IsEnabled)
        {
            return Result.Ok();
        }

        IsEnabled = false;
        ModifiedAtUtc = modifiedAtUtc;

        // TODO: Domain event?

        return Result.Ok();
    }

    private static Result ValidateSchedule(int scheduleTypeId, DateTime? runAtUtc, CronExpression? cronExpression)
    {
        var selectedType = (ScheduleTypeId)scheduleTypeId;

        if (!Enum.IsDefined(selectedType))
        {
            return Result.Fail(ScheduleErrors.InvalidScheduleType);
        }

        if (selectedType == ScheduleTypeId.OneOff)
        {
            if (cronExpression is not null)
            {
                return Result.Fail(ScheduleErrors.OneOffWithCron);
            }
            else if (runAtUtc is null)
            {
                return Result.Fail(ScheduleErrors.OneOffMissingRunDate);
            }
        }
        else if (selectedType == ScheduleTypeId.Recurring)
        {
            if (runAtUtc is not null)
            {
                return Result.Fail(ScheduleErrors.RecurringWithRunDate);
            }
            else if (cronExpression is null)
            {
                return Result.Fail(ScheduleErrors.RecurringMissingCron);
            }
        }
        else
        {
            throw new NotImplementedException($"Schedule type of {scheduleTypeId} is not supported");
        }

        return Result.Ok();
    }
}


