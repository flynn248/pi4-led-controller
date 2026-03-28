using FluentResults;

namespace Led.Domain.Tenants.ValueObjects;

public sealed record Name
{
    private Name(string value) => Value = value;
    public string Value { get; init; }
    public const int MaxLength = 100;

    public static Result<Name> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<Name>(NameErrors.Empty);
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<Name>(NameErrors.InvalidLength(MaxLength));
        }

        return new Name(value);
    }
}
