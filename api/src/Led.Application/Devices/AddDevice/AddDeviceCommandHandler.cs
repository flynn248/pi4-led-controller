using FluentResults;
using Led.Domain.Devices;
using Led.Domain.Devices.Repositories;
using Led.Domain.Devices.ValueObjects;
using Led.SharedKernal.Clock;
using Led.SharedKernal.UoW;
using LiteBus.Commands.Abstractions;

namespace Led.Application.Devices.AddDevice;

internal sealed class AddDeviceCommandHandler(IUnitOfWorkManager unitOfWorkManager, IDateTimeProvider dateTimeProvider, IDeviceRepository deviceRepository) : ICommandHandler<AddDeviceCommand, Result>
{
    public async Task<Result> HandleAsync(AddDeviceCommand message, CancellationToken cancellationToken = default)
    {
        var name = DeviceName.Create(message.Name);
        var ipAddress = DeviceIpAddress.Create(message.IpAddress);
        var serial = SerialNumber.Create(message.SerialNumber);
        var desc = Description.Create(message.Description);

        var overall = Result.Merge(name, ipAddress, serial, desc);

        if (overall.IsFailed)
        {
            return Result.Fail(overall.Errors);
        }

        // IP Address needs to be unique for Tenant
        // Serial number needs to be unique across the board
        using var uow = unitOfWorkManager.Begin();

        var doesIpExist = await deviceRepository.DoesIpAddressExist(message.TenantId, message.IpAddress, cancellationToken);
        if (doesIpExist)
        {
            return Result.Fail("Ip address already exists"); // TODO: Error thing conflict
        }

        var doesSerialExist = await deviceRepository.DoesSerialNumberExist(message.SerialNumber, cancellationToken);

        if (doesSerialExist)
        {
            return Result.Fail("Device already registered"); // TODO: Error thing conflict
        }

        var device = Device.Create(message.TenantId, name.Value, ipAddress.Value, serial.Value, desc.Value, dateTimeProvider.UtcNow);

        deviceRepository.Add(device);

        await uow.SaveChanges(cancellationToken);

        return Result.Ok();
    }
}
