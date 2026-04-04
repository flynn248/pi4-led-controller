using System.Net;
using Led.Domain.Abstraction;

namespace Led.Domain.Devices.Repositories;

public interface IDeviceRepository : IRepository<Device, Guid>
{
    Task<bool> DoesSerialNumberExist(string serialNumber, CancellationToken cancellationToken = default);
    Task<bool> DoesIpAddressExist(Guid tenantId, IPAddress ipAddress, CancellationToken cancellationToken = default);
}
