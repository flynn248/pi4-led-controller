using FluentResults;

namespace Led.Domain.Devices.ValueObjects;

public sealed record Hostname
{
    private Hostname(string value) => Value = value;
    public string Value { get; init; }
    public const int MaxLength = 200;

    public static Result<Hostname> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<Hostname>(HostnameErrors.Empty);
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<Hostname>(HostnameErrors.InvalidLength(MaxLength));
        }

        return new Hostname(value);
    }
}
