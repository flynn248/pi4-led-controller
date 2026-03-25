using FluentResults;
using Led.Domain.Tenants;
using Led.Domain.Tenants.Repositories;
using Led.Domain.Tenants.ValueObjects;
using Led.SharedKernal.Clock;
using Led.SharedKernal.UoW;
using LiteBus.Commands.Abstractions;

namespace Led.Application.Users.UpdateEmail;

public sealed class UpdateEmailCommandHandler(IUserRepository userRepository,
                                              IUnitOfWorkManager unitOfWorkManager,
                                              IDateTimeProvider dateTimeProvider) : ICommandHandler<UpdateEmailCommand, Result>
{
    public async Task<Result> HandleAsync(UpdateEmailCommand message, CancellationToken cancellationToken = default)
    {
        using var uow = unitOfWorkManager.Begin();

        var user = await userRepository.GetById(message.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Fail(UserError.NotFound);
        }

        var isDupe = await userRepository.IsDuplicateEmail(message.UserId, message.NewEmail, cancellationToken);

        if (isDupe)
        {
            return Result.Fail(EmailErrors.Duplicate);
        }

        var newEmail = Email.Create(message.NewEmail);

        if (newEmail.IsFailed)
        {
            return newEmail.ToResult();
        }

        user.UpdateEmail(newEmail.Value, dateTimeProvider.UtcNow);

        await uow.SaveChanges(cancellationToken);

        return Result.Ok();
    }
}
