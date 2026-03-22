using FluentResults;

namespace Led.Domain.Users.ValueObjects;

public sealed record Email
{
    private Email(string value) => Value = value;

    public string Value { get; init; }

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<Email>(EmailErrors.Empty);
        }

        value = value.Trim();

        // Email format validation
        if (value.Split('@').Length != 2 || !value.Contains('.'))
        {
            return Result.Fail<Email>(EmailErrors.InvalidFormat);
        }

        // TODO: Add more robust email validation

        return new Email(value);
    }
}
