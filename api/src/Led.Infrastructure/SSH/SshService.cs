using Led.Application.Abstraction.SSH;
using Microsoft.Extensions.Logging;
using Renci.SshNet;

namespace Led.Infrastructure.SSH;

internal sealed class SshService(ILogger<SshService> logger) : ISshService
{
    public async Task<string> GetLinuxDeviceCpuSerialNumber(string host, string username, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            using var client = new SshClient(host, username, password);

            await client.ConnectAsync(cancellationToken);

            var command = "cat /sys/firmware/devicetree/base/serial-number";

            using var cmd = client.RunCommand(command);

            var result = cmd.Result;

            client.Disconnect();

            return result.Trim().TrimEnd('\0');
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{MethName} encountered an error", nameof(GetLinuxDeviceCpuSerialNumber));
            return string.Empty;
        }
    }
}
