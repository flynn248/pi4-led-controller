using FluentResults;
using Led.Domain.Devices.Events;
using Led.Domain.Devices.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.Devices;

public sealed class Device : AggregateRoot<Guid>
{
    public Guid TenantId { get; private set; }
    public Hostname Hostname { get; private set; }
    public DeviceIpAddress IpAddress { get; private set; }
    public SerialNumber SerialNumber { get; private set; }
    public Description Description { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? ModifiedAtUtc { get; private set; }
    public DateTime LastSeenAtUtc { get; private set; }

    private Device()
    { }

    private Device(Guid id, Guid tenantId, Hostname hostname, DeviceIpAddress ipAddress, SerialNumber serialNumber, Description description, DateTime createdAtUtc)
    {
        TenantId = tenantId;
        Hostname = hostname;
        IpAddress = ipAddress;
        SerialNumber = serialNumber;
        Description = description;
        CreatedAtUtc = createdAtUtc;
    }

    public static Result<Device> Create(Guid userId, Hostname hostname, DeviceIpAddress ipAddress, SerialNumber serialNumber, Description description, DateTime createdAtUtc)
    {
        return new Device(Guid.CreateVersion7(), userId, hostname, ipAddress, serialNumber, description, createdAtUtc);
    }

    public void UpdateHostname(Hostname newhostname, DateTime modifiedAtUtc)
    {
        Hostname = newhostname;
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
