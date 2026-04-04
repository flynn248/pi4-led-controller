using FluentResults;
using Led.Domain.Devices;
using Led.Domain.Devices.EntityErrors;
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
        // TODO: Consider this endpoint both verifying connection and adding to simplify flow and avoid moving/ relying on serial number back to UI
        // IP Address needs to be unique for Tenant
        // Serial number needs to be unique across the board
        using var uow = unitOfWorkManager.Begin();

        var doesIpExist = await deviceRepository.DoesIpAddressExist(message.TenantId, ipAddress.Value.Value, cancellationToken);
        if (doesIpExist)
        {
            return Result.Fail(DeviceErrors.IPAddressExists);
        }

        var doesSerialExist = await deviceRepository.DoesSerialNumberExist(message.SerialNumber, cancellationToken);

        if (doesSerialExist)
        {
            return Result.Fail(DeviceErrors.SerialNumerExists);
        }

        var device = Device.Create(message.TenantId, name.Value, ipAddress.Value, serial.Value, desc.Value, dateTimeProvider.UtcNow);

        deviceRepository.Add(device);

        await uow.SaveChanges(cancellationToken);

        return Result.Ok();
    }
}
