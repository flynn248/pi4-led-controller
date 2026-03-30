using FluentResults;
using NCrontab;

namespace Led.Domain.Schedules.ValueObjects;

public sealed record CronExpression
{
    private CronExpression(string? value) => Value = value;
    public string? Value { get; init; }
    public const int MaxLength = 80;
    public static CronExpression Empty => new((string?)null);

    public static Result<CronExpression> Create(string? cronExp)
    {
        if (string.IsNullOrWhiteSpace(cronExp))
        {
            return Empty;
        }

        cronExp = cronExp.Trim();

        if (cronExp.Length > MaxLength)
        {
            return Result.Fail<CronExpression>(CronExpressionErrors.InvalidLength(MaxLength));
        }

        var schedule = CrontabSchedule.TryParse(cronExp);

        if (schedule is null)
        {
            return Result.Fail<CronExpression>(CronExpressionErrors.Invalid);
        }

        return new CronExpression(cronExp);
    }
}
