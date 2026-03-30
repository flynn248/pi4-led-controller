using FluentResults;

namespace Led.Domain.Schedules.ValueObjects;

public sealed record ScheduleLabel
{
    private ScheduleLabel(string? value) => Value = value;
    public string? Value { get; init; }

    public const int MaxLength = 100;
    public static ScheduleLabel Empty => new((string?)null);

    public static Result<ScheduleLabel> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Empty;
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<ScheduleLabel>(ScheduleLabelErrors.InvalidLength(MaxLength));
        }

        return new ScheduleLabel(value);
    }
}
