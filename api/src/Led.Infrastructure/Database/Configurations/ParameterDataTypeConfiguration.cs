using Led.Domain.EffectTypes;
using Led.Domain.EffectTypes.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Led.Infrastructure.Database.Configurations;

internal sealed class ParameterDataTypeConfiguration : IEntityTypeConfiguration<ParameterDataType>
{
    public void Configure(EntityTypeBuilder<ParameterDataType> builder)
    {
        builder.ToTable("parameter_data_type");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasConversion<short>();

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(ParameterDataType.NameMaxLength);

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(ParameterDataType.DescriptionMaxLength);

        builder.HasData(GetSeedData());
    }

    private ParameterDataType[] GetSeedData()
    {
        return [
            ParameterDataType.Create(ParameterDataTypeId.Boolean, "True/ False", "Property represents a true or false value"),
            ParameterDataType.Create(ParameterDataTypeId.WholeNumber, "Whole Number", "Property represents a whole number. Ex., 1, 2, 3, ..."),
            ParameterDataType.Create(ParameterDataTypeId.RationalNumber, "Rational Number", "Property represents a whole or floating point number. Ex., 1, 2, 2.5, 3.8, ..."),
            ParameterDataType.Create(ParameterDataTypeId.Collection, "Collection", "Property represents a collection of alpha-numeric characters. Ex., \"abc\", \"123abc\", ..."),
            ParameterDataType.Create(ParameterDataTypeId.Complex, "Complex", "Property represents the parent to one or more child properties")
            ];
    }
}
