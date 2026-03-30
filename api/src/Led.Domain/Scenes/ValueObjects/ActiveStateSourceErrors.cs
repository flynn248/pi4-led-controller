using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.Scenes.ValueObjects;

public static class ActiveStateSourceErrors
{
    private const string _baseErrorCode = "active_state.source";
    public const string EmptyErrorCode = $"{_baseErrorCode}.empty";
    public const string InvalidLengthErrorCode = $"{_baseErrorCode}.invalid_length";

    public static Error Empty => new Error("Source cannot be empty").Validation(EmptyErrorCode);
    public static Error InvalidLength(int max) => new Error($"Source cannot exceed {max} characters").Validation(InvalidLengthErrorCode);
}
