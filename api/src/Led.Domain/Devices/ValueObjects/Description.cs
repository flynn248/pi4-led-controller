using FluentResults;

namespace Led.Domain.Devices.ValueObjects;

public sealed record Description
{
    private Description(string value) => Value = value;
    public string Value { get; init; }
    public const int MaxLength = 200;

    public static Result<Description> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new Description(string.Empty);
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<Description>(DescriptionErrors.InvalidLength(MaxLength));
        }

        return new Description(value);
    }
}
