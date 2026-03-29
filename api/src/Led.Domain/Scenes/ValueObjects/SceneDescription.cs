using FluentResults;

namespace Led.Domain.Scenes.ValueObjects;

public sealed record SceneDescription
{
    private SceneDescription(string value) => Value = value;
    public string Value { get; init; }
    public const int MaxLength = 200;

    public static Result<SceneDescription> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new SceneDescription(string.Empty);
        }

        value = value.Trim();

        if (value.Length > MaxLength)
        {
            return Result.Fail<SceneDescription>(SceneDescriptionErrors.InvalidLength(MaxLength));
        }

        return new SceneDescription(value);
    }
}
