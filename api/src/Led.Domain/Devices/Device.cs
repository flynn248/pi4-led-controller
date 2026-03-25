using FluentResults;
using Led.Domain.Devices.Events;
using Led.Domain.Devices.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.Devices;

public sealed class Device : AggregateRoot<Guid>
{
    public Guid TenantId { get; private set; }
    public Hostname Hostname { get; private set; }
    public Description Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public DateTime LastSeenAt { get; private set; }

    private Device()
    { }

    private Device(Guid id, Guid tenantId, Hostname hostname, Description description, DateTime createdAt)
    {
        TenantId = tenantId;
        Hostname = hostname;
        Description = description;
        CreatedAt = createdAt;
    }

    public static Result<Device> Create(Guid userId, Hostname hostname, Description description, DateTime createdAt)
    {
        return new Device(Guid.CreateVersion7(), userId, hostname, description, createdAt);
    }

    public void UpdateHostname(Hostname newhostname, DateTime modifiedAt)
    {
        Hostname = newhostname;
        ModifiedAt = modifiedAt;

        RaiseDomainEvent(new DeviceHostnameUpdatedDomainEvent(Id));
    }

    public void UpdateDescription(Description newDescription, DateTime modifiedAt)
    {
        Description = newDescription;
        ModifiedAt = modifiedAt;
    }

    public void UpdateLastSeen(DateTime lastSeenAt)
    {
        LastSeenAt = lastSeenAt;
    }
}
