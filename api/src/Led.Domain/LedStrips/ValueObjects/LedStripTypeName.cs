namespace Led.Domain.LedStrips.ValueObjects;

public sealed record LedStripTypeName(string TypeName)
{
    public const int MaxLength = 50;
}
