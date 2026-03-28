using FluentResults;

namespace Led.Domain.Tenants.ValueObjects;

public sealed record Username
{
    private Username(string value) => Value = value;
    public string Value { get; init; }
    public const int MinLength = 3;
    public const int MaxLength = 20;

    public static Result<Username> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<Username>(UsernameErrors.Empty);
        }

        value = value.Trim();

        if (value.Length < MinLength || value.Length > MaxLength)
        {
            return Result.Fail<Username>(UsernameErrors.InvalidLength(MinLength, MaxLength));
        }

        return new Username(value);
    }
}
