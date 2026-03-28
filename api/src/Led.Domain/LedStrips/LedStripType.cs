using Led.Domain.LedStrips.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.LedStrips;

public sealed class LedStripType : Entity<LedStripTypeId>
{
    public LedStripTypeName TypeName { get; init; }

    private LedStripType()
    { }

    internal LedStripType(LedStripTypeName typeName)
    {
        TypeName = typeName;
    }
}
