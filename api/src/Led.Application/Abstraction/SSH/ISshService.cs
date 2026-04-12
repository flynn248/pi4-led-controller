using FluentResults;

namespace Led.Application.Abstraction.SSH;

public interface ISshService
{
    Task<Result<string>> GetLinuxDeviceCpuSerialNumber(string host, string username, string password, CancellationToken cancellationToken = default);
}
