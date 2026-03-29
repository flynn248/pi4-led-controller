using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Scenes.ValueObjects;

public static class SceneDescriptionErrors
{
    private const string _baseErrorCode = "scene.description";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";

    public static Error InvalidLength(int max) => new Error($"Description cannot exceed {max} characters").Validation(InvalidLengthErrorCode);
}
