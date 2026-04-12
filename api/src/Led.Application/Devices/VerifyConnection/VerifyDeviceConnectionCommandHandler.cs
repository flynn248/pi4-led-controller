using FluentResults;
using Led.Application.Abstraction.SSH;
using LiteBus.Commands.Abstractions;

namespace Led.Application.Devices.VerifyConnection;

internal sealed class VerifyDeviceConnectionCommandHandler(ISshService sshService) : ICommandHandler<VerifyDeviceConnectionCommand, Result<bool>>
{
    public async Task<Result<bool>> HandleAsync(VerifyDeviceConnectionCommand message, CancellationToken cancellationToken = default)
    {
        var res = await sshService.GetLinuxDeviceCpuSerialNumber(message.IpAddress, message.Username, message.Password, cancellationToken);

        if (res.IsFailed)
        {
            return Result.Fail(res.Errors);
        }

        return !string.IsNullOrWhiteSpace(res.Value);
    }
}
