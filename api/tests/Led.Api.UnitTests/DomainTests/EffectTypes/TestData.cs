using Led.Domain.EffectTypes;
using Led.Domain.EffectTypes.ValueObjects;
using Led.Domain.Shared.ValueObjects;

namespace Led.Api.UnitTests.DomainTests.EffectTypes;

internal static class TestData
{
    public static EffectType CreateEffectType(DateTime createdAtUtc) => EffectType.Create(Guid.CreateVersion7(),
                                                                                          EffectTypeName.Create("name").Value,
                                                                                          EffectTypeDescription.Create("description").Value,
                                                                                          false,
                                                                                          false,
                                                                                          PosNum<int>.Create(1).Value,
                                                                                          createdAtUtc);

    public static EffectParameterSchema CreateEffectParameterSchema() => EffectParameterSchema.Create(Guid.CreateVersion7(),
                                                                                                      ParameterKey.Create("key").Value,
                                                                                                      ParameterDataTypeId.Boolean,
                                                                                                      true,
                                                                                                      null,
                                                                                                      null,
                                                                                                      ParameterAllowedValues.Empty,
                                                                                                      ParameterDescription.Empty).Value;
}
