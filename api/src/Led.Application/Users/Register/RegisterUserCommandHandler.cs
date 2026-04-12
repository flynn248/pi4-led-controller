using FluentResults;
using Led.Domain.Tenants;
using Led.Domain.Tenants.EntityErrors;
using Led.Domain.Tenants.Repositories;
using Led.Domain.Tenants.ValueObjects;
using Led.SharedKernal.Clock;
using Led.SharedKernal.UoW;
using LiteBus.Commands.Abstractions;

namespace Led.Application.Users.Register;

internal sealed class RegisterUserCommandHandler(IDateTimeProvider dateTimeProvider,
                                                 IUserRepository userRepository,
                                                 IUnitOfWorkManager unitOfWorkManager) : ICommandHandler<RegisterUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> HandleAsync(RegisterUserCommand message, CancellationToken cancellationToken = default)
    {
        var email = Email.Create(message.Email);
        var username = Username.Create(message.Username);
        var firstName = Name.Create(message.FirstName);
        var lastName = Name.Create(message.LastName);

        var overall = Result.Merge(email, username, firstName, lastName);

        if (overall.IsFailed)
        {
            return Result.Fail(overall.Errors);
        }

        using var uow = unitOfWorkManager.Begin();

        var isDupeEmail = await userRepository.IsDuplicateEmail(email.Value.Value, cancellationToken);

        if (isDupeEmail)
        {
            return Result.Fail(UserError.EmailExists);
        }

        var isDupeUsename = await userRepository.IsDuplicateUsername(username.Value.Value, cancellationToken);

        if (isDupeUsename)
        {
            return Result.Fail(UserError.UsernameExists);
        }

        var user = User.Create(firstName.Value, lastName.Value, username.Value, email.Value, message.Password, dateTimeProvider.UtcNow);

        userRepository.Add(user);
        // TODO: Doing optimistic concurrency
        await uow.SaveChanges(cancellationToken);

        return Result.Ok(user.Id);
    }
}
