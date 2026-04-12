using FluentResults;
using Led.Application.Abstraction.SSH;
using Led.SharedKernal.FluentResult;
using Microsoft.Extensions.Logging;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace Led.Infrastructure.SSH;

internal sealed class SshService(ILogger<SshService> logger) : ISshService
{
    public async Task<Result<string>> GetLinuxDeviceCpuSerialNumber(string host, string username, string password, CancellationToken cancellationToken = default)
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
        catch (SshAuthenticationException ex)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation(ex, "{MethName} encountered an error", nameof(GetLinuxDeviceCpuSerialNumber));
            }

            return Result.Fail(SshServiceErrors.Authentication);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{MethName} encountered an error", nameof(GetLinuxDeviceCpuSerialNumber));
            return Result.Fail(SshServiceErrors.Failure);
        }
    }
}

internal static class SshServiceErrors
{
    private const string _baseErrorCode = "ssh";
    public const string EmptyErrorCode = $"{_baseErrorCode}.failure";
    public const string AuthenticationErrorCode = $"{_baseErrorCode}.authentication";

    public static Error Failure => new Error("Unknown error occurred").Failure(EmptyErrorCode);
    public static Error Authentication => new Error("Error authenticating with device. Please verify username/ password").Validation(AuthenticationErrorCode);
}
