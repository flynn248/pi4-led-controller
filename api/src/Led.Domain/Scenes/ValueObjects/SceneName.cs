using FluentResults;

namespace Led.Domain.Scenes.ValueObjects;

public sealed record SceneName
{
    private SceneName(string value) => Value = value;
    public string Value { get; init; }
    public const int MaxLength = 80;

    public static Result<SceneName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<SceneName>(SceneNameErrors.Empty);
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<SceneName>(SceneNameErrors.InvalidLength(MaxLength));
        }

        return new SceneName(value);
    }
}
