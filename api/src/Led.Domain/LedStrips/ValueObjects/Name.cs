using FluentResults;

namespace Led.Domain.LedStrips.ValueObjects;

public sealed record Name
{
    private Name(string value) => Value = value;
    public string Value { get; init; }

    public static Result<Name> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<Name>(NameErrors.Empty);
        }

        value = value.Trim();

        return new Name(value);
    }
}
