using FluentResults;
using Led.Domain.Users.Events;
using Led.Domain.Users.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.Users;

public sealed class User : AggregateRoot<Guid>
{
    public Email Email { get; private set; }
    public Username Username { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }

    private User()
    { }

    private User(Guid id, Email email, Username userName, string passwordHash, DateTime createdAt)
        : base(id)
    {
        Email = email;
        Username = userName;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
    }

    public static Result<User> Create(Email email, Username userName, string passwordHash, DateTime createdAt)
    {
        var user = new User(Guid.CreateVersion7(), email, userName, passwordHash, createdAt);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    public Result UpdateEmail(Email newEmail, DateTime modifiedAt)
    {
        Email oldEmail = Email;

        Email = newEmail;
        ModifiedAt = modifiedAt;

        RaiseDomainEvent(new UserEmailUpdatedDomainEvent(Id, Email.Value, oldEmail.Value));

        return Result.Ok();
    }

    public Result UpdateUserName(Username newUsername, DateTime modifiedAt)
    {
        Username = newUsername;
        ModifiedAt = modifiedAt;

        RaiseDomainEvent(new UserUsernameUpdatedDomainEvent(Id));

        return Result.Ok();
    }
}
