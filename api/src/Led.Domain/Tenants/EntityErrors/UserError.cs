using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Tenants.EntityErrors;

public static class UserError
{
    private const string _baseErrorCode = "user";
    public const string NotFoundErrorCode = $"{_baseErrorCode}.not_found";

    public static Error NotFound => new Error("User not found").NotFound(NotFoundErrorCode);
}
