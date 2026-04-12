using FluentResults;
using LiteBus.Commands.Abstractions;

namespace Led.Application.Tenants.AddTenant;

// TODO: Remove UserId and pull from JWT once in place
public sealed record AddTenantCommand(Guid UserId, string TenantName) : ICommand<Result<Guid>>;
