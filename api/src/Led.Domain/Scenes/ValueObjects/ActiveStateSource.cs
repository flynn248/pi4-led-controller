using FluentResults;

namespace Led.Domain.Scenes.ValueObjects;

public sealed record ActiveStateSource
{
    private ActiveStateSource(string value) => Value = value;
    public string Value { get; init; }
    public const int MaxLength = 50;

    public static Result<ActiveStateSource> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<ActiveStateSource>(ActiveStateSourceErrors.Empty);
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<ActiveStateSource>(ActiveStateSourceErrors.InvalidLength(MaxLength));
        }

        return new ActiveStateSource(value);
    }
}
