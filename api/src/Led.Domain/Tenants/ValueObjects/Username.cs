using FluentResults;

namespace Led.Domain.Tenants.ValueObjects;

public sealed record Username
{
    private Username(string value) => Value = value;
    public string Value { get; init; }

    public static Result<Username> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<Username>(UsernameErrors.Empty);
        }

        value = value.Trim();

        if (value.Length < 3 || value.Length > 20)
        {
            return Result.Fail<Username>(UsernameErrors.InvalidLength(3, 20));
        }

        if (value.Contains("brandon")) // TODO: Replace with actual format validation (e.g., regex)
        {
            return Result.Fail<Username>(UsernameErrors.InvalidFormat);
        }

        return new Username(value);
    }
}
