using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Scenes.ValueObjects;

public static class SceneNameErrors
{
    private const string _baseErrorCode = "scene.name";
    public const string EmptyErrorCode = $"{_baseErrorCode}.empty";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";

    public static Error Empty => new Error("Scene name cannot be empty").Validation(EmptyErrorCode);
    public static Error InvalidLength(int length) => new Error($"Scene name cannot exceed {length} characters").Validation(InvalidLengthErrorCode);
}
