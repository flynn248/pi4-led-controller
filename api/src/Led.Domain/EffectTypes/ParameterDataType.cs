using Led.Domain.EffectTypes.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.EffectTypes;

// Added for EF Core configuration of lookup table

public sealed class ParameterDataType : Entity<ParameterDataTypeId>
{
    public const int NameMaxLength = 16;
    public const int DescriptionMaxLength = 200;

    public string Name { get; init; }
    public string Description { get; init; }

    private ParameterDataType()
    { }

    private ParameterDataType(ParameterDataTypeId id, string name, string description)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public static ParameterDataType Create(ParameterDataTypeId id, string name, string description)
    {
        name = name.Trim();

        if (name.Length > NameMaxLength)
        {
            throw new InvalidOperationException($"Name cannot exceed {NameMaxLength} characters");
        }

        description = description.Trim();

        if (description.Length > DescriptionMaxLength)
        {
            throw new InvalidOperationException($"Name cannot exceed {DescriptionMaxLength} characters");
        }

        return new ParameterDataType(id, name, description);
    }
}
