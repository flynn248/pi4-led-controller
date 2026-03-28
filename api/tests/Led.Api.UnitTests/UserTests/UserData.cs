using Led.Domain.Tenants;
using Led.Domain.Tenants.ValueObjects;

namespace Led.Api.UnitTests.UserTests;

internal static class UserData
{
    public static User Create(DateTime createdAtUtc) => User.Create(FirstName, LastName, Username, Email, PasswordHash, createdAtUtc);

    public static Name FirstName = Name.Create("First").Value;
    public static Name LastName = Name.Create("Last").Value;
    public static Username Username = Username.Create("username").Value;
    public static Email Email = Email.Create("valid@valid.com").Value;
    public static string PasswordHash = "secret_password";
}
