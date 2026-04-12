using FluentResults;
using Led.Application.Abstraction.SSH;
using Led.Domain.Devices;
using Led.Domain.Devices.EntityErrors;
using Led.Domain.Devices.Repositories;
using Led.Domain.Devices.ValueObjects;
using Led.SharedKernal.Clock;
using Led.SharedKernal.UoW;
using LiteBus.Commands.Abstractions;

namespace Led.Application.Devices.AddDevice;

internal sealed class AddDeviceCommandHandler(IUnitOfWorkManager unitOfWorkManager,
                                              IDateTimeProvider dateTimeProvider,
                                              IDeviceRepository deviceRepository,
                                              ISshService sshService) : ICommandHandler<AddDeviceCommand, Result>
{
    public async Task<Result> HandleAsync(AddDeviceCommand message, CancellationToken cancellationToken = default)
    {
        var serialResponse = await sshService.GetLinuxDeviceCpuSerialNumber(message.IpAddress, message.Username, message.Password, cancellationToken);
        if (serialResponse.IsFailed)
        {
            return Result.Fail(serialResponse.Errors);
        }

        var name = DeviceName.Create(message.Name);
        var ipAddress = DeviceIpAddress.Create(message.IpAddress);
        var serial = SerialNumber.Create(serialResponse.Value);
        var desc = Description.Create(message.Description);

        var overall = Result.Merge(name, ipAddress, serial, desc);

        if (overall.IsFailed)
        {
            return Result.Fail(overall.Errors);
        }

        using var uow = unitOfWorkManager.Begin();

        var doesIpExist = await deviceRepository.DoesIpAddressExist(message.TenantId, ipAddress.Value.Value, cancellationToken);
        if (doesIpExist)
        {
            return Result.Fail(DeviceErrors.IPAddressExists);
        }

        var doesSerialExist = await deviceRepository.DoesSerialNumberExist(serial.Value.Value, cancellationToken);

        if (doesSerialExist)
        {
            return Result.Fail(DeviceErrors.SerialNubmerExists);
        }

        var device = Device.Create(message.TenantId, name.Value, ipAddress.Value, serial.Value, desc.Value, dateTimeProvider.UtcNow);

        deviceRepository.Add(device);

        // TODO: Using optimistic concurrency atm. Need to handle concurrent request
        await uow.SaveChanges(cancellationToken);

        return Result.Ok();
    }
}
