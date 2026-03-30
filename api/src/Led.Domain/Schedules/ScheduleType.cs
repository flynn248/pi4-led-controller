using Led.Domain.Schedules.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.Schedules;

// Added for EF Core configuration of lookup table

public sealed class ScheduleType : Entity<ScheduleTypeId>
{
    public string Name { get; init; }

    public const int NameMaxLength = 16;

    private ScheduleType()
    { }

    private ScheduleType(ScheduleTypeId id, string name)
        : base(id)
    {
        Name = name;
    }

    public static ScheduleType Create(ScheduleTypeId id, string name)
    {
        name = name.Trim();

        if (name.Length > NameMaxLength)
        {
            throw new InvalidOperationException($"Name cannot exceed {NameMaxLength} characters");
        }

        return new ScheduleType(id, name);
    }
}
