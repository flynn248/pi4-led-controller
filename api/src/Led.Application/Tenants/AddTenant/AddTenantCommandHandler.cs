using FluentResults;
using Led.Domain.Tenants;
using Led.Domain.Tenants.Repositories;
using Led.Domain.Tenants.ValueObjects;
using Led.SharedKernal.Clock;
using Led.SharedKernal.UoW;
using LiteBus.Commands.Abstractions;

namespace Led.Application.Tenants.AddTenant;

internal sealed class AddTenantCommandHandler(IUnitOfWorkManager unitOfWorkManager,
                                              IDateTimeProvider dateTimeProvider,
                                              ITenantRepository tenantRepository) : ICommandHandler<AddTenantCommand, Result<Guid>>
{
    public async Task<Result<Guid>> HandleAsync(AddTenantCommand message, CancellationToken cancellationToken = default)
    {
        var tenantName = Name.Create(message.TenantName);

        if (tenantName.IsFailed)
        {
            return Result.Fail(tenantName.Errors);
        }

        var newTenant = Tenant.Create(message.UserId, tenantName.Value, dateTimeProvider.UtcNow);

        using var uow = unitOfWorkManager.Begin();

        tenantRepository.Add(newTenant);

        await uow.SaveChanges(cancellationToken);

        return newTenant.Id;
    }
}
