using FluentResults;

namespace Led.Domain.Devices.ValueObjects;

public sealed record Hostname
{
    private Hostname(string value) => Value = value;
    public string Value { get; init; }

    public static Result<Hostname> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<Hostname>(HostnameErrors.Empty);
        }

        value = value.Trim();

        return new Hostname(value);
    }
}
