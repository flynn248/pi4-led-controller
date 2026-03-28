using FluentResults;

namespace Led.Domain.Tenants.ValueObjects;

public sealed record Email
{
    private Email(string value) => Value = value;
    public string Value { get; init; }
    public const int MaxLength = 100;

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<Email>(EmailErrors.Empty);
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<Email>(EmailErrors.InvalidLength(MaxLength));
        }

        // Email format validation
        if (value.Split('@').Length != 2 || !value.Contains('.'))
        {
            return Result.Fail<Email>(EmailErrors.InvalidFormat);
        }

        // TODO: Add more robust email validation

        return new Email(value);
    }
}
