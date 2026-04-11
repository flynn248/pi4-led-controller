using Led.Domain.EffectTypes;
using Led.Domain.EffectTypes.ValueObjects;
using Led.Domain.Shared.ValueObjects;

namespace Led.Api.UnitTests.Domain.EffectTypes;

internal static class TestData
{
    public static EffectType CreateEffectType(DateTime createdAtUtc) => EffectType.Create(Guid.CreateVersion7(),
                                                                                          EffectTypeName.Create("name").Value,
                                                                                          EffectTypeDescription.Create("description").Value,
                                                                                          false,
                                                                                          false,
                                                                                          PosNum<int>.Create(1).Value,
                                                                                          createdAtUtc);
}
