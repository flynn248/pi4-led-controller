using FluentResults;
using LiteBus.Commands.Abstractions;

namespace Led.Application.Devices.VerifyConnection;

public sealed record VerifyDeviceConnectionCommand(string IpAddress, string Username, string Password) : ICommand<Result<string>>;
