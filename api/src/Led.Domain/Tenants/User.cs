using Led.Domain.Tenants.Events;
using Led.Domain.Tenants.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.Tenants;

public sealed class User : AggregateRoot<Guid>
{
    public Email Email { get; private set; }
    public Name FirstName { get; private set; }
    public Name LastName { get; private set; }
    public Username Username { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }

    private User()
    { }

    private User(Guid id, Name firstName, Name lastName, Username username, Email email, string passwordHash, DateTime createdAt)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
    }

    public static User Create(Name firstName, Name lastName, Username username, Email email, string passwordHash, DateTime createdAt)
    {
        var user = new User(Guid.CreateVersion7(), firstName, lastName, username, email, passwordHash, createdAt);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    public void UpdateEmail(Email newEmail, DateTime modifiedAt)
    {
        if (Email == newEmail)
        {
            return;
        }

        Email oldEmail = Email;

        Email = newEmail;
        ModifiedAt = modifiedAt;

        RaiseDomainEvent(new UserEmailUpdatedDomainEvent(Id, Email.Value, oldEmail.Value));
    }

    public void UpdateUserName(Username newUsername, DateTime modifiedAt)
    {
        if (Username == newUsername)
        {
            return;
        }

        Username = newUsername;
        ModifiedAt = modifiedAt;

        RaiseDomainEvent(new UserUsernameUpdatedDomainEvent(Id));
    }

    public void UpdateName(Name newFirstName, Name newLastName, DateTime modifiedAt)
    {
        if (FirstName == newFirstName && LastName == newLastName)
        {
            return;
        }

        FirstName = newFirstName;
        LastName = newLastName;
        ModifiedAt = modifiedAt;
    }
}
