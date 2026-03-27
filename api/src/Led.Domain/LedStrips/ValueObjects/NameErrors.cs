using FluentResults;
using Led.SharedKernal.FluentResult;

namespace Led.Domain.LedStrips.ValueObjects;

public static class NameErrors
{
    private const string _baseErrorCode = "name";
    public const string EmptyErrorCode = $"{_baseErrorCode}.empty";

    public static Error Empty => new Error("Name cannot be empty").Validation(EmptyErrorCode);
}
