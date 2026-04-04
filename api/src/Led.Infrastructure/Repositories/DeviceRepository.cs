using System.Net;
using Led.Domain.Devices;
using Led.Domain.Devices.Repositories;
using Led.Infrastructure.Database;
using Led.Infrastructure.Database.Abstraction;
using Led.Infrastructure.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Led.Infrastructure.Repositories;

internal sealed class DeviceRepository : Repository<ApplicationDbContext, Device, Guid>, IDeviceRepository
{
    public DeviceRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<bool> DoesIpAddressExist(Guid tenantId, IPAddress ipAddress, CancellationToken cancellationToken = default)
    {
        var dbContext = GetDbContext();

        return await dbContext.Set<Device>().Where(d => d.TenantId == tenantId && d.IpAddress.Value == ipAddress).AnyAsync(cancellationToken);
    }

    public Task<bool> DoesSerialNumberExist(string serialNumber, CancellationToken cancellationToken = default)
    {
        var dbContext = GetDbContext();

        return dbContext.Set<Device>().AnyAsync(d => d.SerialNumber.Value == serialNumber, cancellationToken);
    }
}
