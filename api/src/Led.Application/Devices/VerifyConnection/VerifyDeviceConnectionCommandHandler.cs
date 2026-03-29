using FluentResults;
using Led.Application.Abstraction.SSH;
using LiteBus.Commands.Abstractions;

namespace Led.Application.Devices.VerifyConnection;

internal sealed class VerifyDeviceConnectionCommandHandler(ISshService sshService) : ICommandHandler<VerifyDeviceConnectionCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(VerifyDeviceConnectionCommand message, CancellationToken cancellationToken = default)
    {
        // TODO: Validation here or in a middleware?
        var res = await sshService.GetLinuxDeviceCpuSerialNumber(message.IpAddress, message.Username, message.Password, cancellationToken);

        return res.Trim();
    }
}
