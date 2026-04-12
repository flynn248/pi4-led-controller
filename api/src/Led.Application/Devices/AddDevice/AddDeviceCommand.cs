using FluentResults;
using LiteBus.Commands.Abstractions;

namespace Led.Application.Devices.AddDevice;

public sealed record AddDeviceCommand(Guid TenantId, string Name, string IpAddress, string Username, string Password, string Description) : ICommand<Result>;
