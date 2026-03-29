using Led.Domain.Devices.Events;
using Led.Domain.Devices.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.Devices;

public sealed class Device : AggregateRoot<Guid>
{
    public Guid TenantId { get; private set; }
    public DeviceName Name { get; private set; }
    public DeviceIpAddress IpAddress { get; private set; }
    public SerialNumber SerialNumber { get; private set; }
    public Description Description { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? ModifiedAtUtc { get; private set; }
    public DateTime LastSeenAtUtc { get; private set; }

    private Device()
    { }

    private Device(Guid id, Guid tenantId, DeviceName name, DeviceIpAddress ipAddress, SerialNumber serialNumber, Description description, DateTime createdAtUtc)
    {
        TenantId = tenantId;
        Name = name;
        IpAddress = ipAddress;
        SerialNumber = serialNumber;
        Description = description;
        CreatedAtUtc = createdAtUtc;
    }

    public static Device Create(Guid tenantId, DeviceName name, DeviceIpAddress ipAddress, SerialNumber serialNumber, Description description, DateTime createdAtUtc)
    {
        return new Device(Guid.CreateVersion7(), tenantId, name, ipAddress, serialNumber, description, createdAtUtc);
    }

    public void UpdateHostname(DeviceName newhostname, DateTime modifiedAtUtc)
    {
        Name = newhostname;
        ModifiedAtUtc = modifiedAtUtc;

        RaiseDomainEvent(new DeviceHostnameUpdatedDomainEvent(Id));
    }

    public void UpdateDescription(Description newDescription, DateTime modifiedAtUtc)
    {
        Description = newDescription;
        ModifiedAtUtc = modifiedAtUtc;
    }

    public void UpdateLastSeen(DateTime lastSeenAtUtc)
    {
        LastSeenAtUtc = lastSeenAtUtc;
    }
}
