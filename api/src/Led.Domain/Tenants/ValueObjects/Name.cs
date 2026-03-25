using FluentResults;
using Led.Domain.LedStrips.ValueObjects;

namespace Led.Domain.Tenants.ValueObjects;

public sealed record Name
{
    private Name(string value) => Value = value;
    public string Value { get; init; }
    public static int MaxLength { get; } = 100;
    public static Result<Name> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<Name>(NameErrors.Empty);
        }
        // TODO: Max length
        value = value.Trim();

        return new Name(value);
    }
}
