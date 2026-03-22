namespace Led.SharedKernal.Clock;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
