using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Tenants.EntityErrors;

public static class UserError
{
    private const string _baseErrorCode = "user";
    public const string NotFoundErrorCode = $"{_baseErrorCode}.not_found";
    public const string EmailAddressExistsErrorCode = $"{_baseErrorCode}.email.exists";
    public const string UsernameExistsErrorCode = $"{_baseErrorCode}.username.exists";

    public static Error EmailExists => new Error("Provided email address already exists").Conflict(EmailAddressExistsErrorCode);
    public static Error UsernameExists => new Error("Provided username already exists").Conflict(EmailAddressExistsErrorCode);
    public static Error NotFound => new Error("User not found").NotFound(NotFoundErrorCode);
}
