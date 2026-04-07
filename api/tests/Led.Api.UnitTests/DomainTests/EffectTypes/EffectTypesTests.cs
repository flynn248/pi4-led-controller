using Led.Domain.Scenes.Events;
using Led.Domain.Shared.ValueObjects;
using Led.SharedKernal.DDD;
using Shouldly;

namespace Led.Api.UnitTests.DomainTests.EffectTypes;

public class EffectTypesTests
{

    [Fact]
    public void UpdateSchemaVersion_Should_RaiseDomainEvent_WithNewSchemaVersion()
    {
        // Arrange
        var effectType = TestData.CreateEffectType(DateTime.UtcNow);

        var newSchemaVersion = PosNum<int>.Create(effectType.SchemaVersion + 1).Value;

        var expectedDomainEvents = new List<IDomainEvent>() { new EffectTypeSchemaUpdatedDomainEvent(effectType.TenantId, effectType.Id, newSchemaVersion) }.AsReadOnly();

        // Act
        effectType.UpdateSchemaVersion(newSchemaVersion, DateTime.UtcNow);

        // Assert
        effectType.GetDomainEvents().ShouldBeEquivalentTo(expectedDomainEvents);
    }

    [Fact]
    public void UpdateSchemaVersion_ShouldNot_RaiseDomainEvent_WithSameSchemaVersion()
    {
        // Arrange
        var effectType = TestData.CreateEffectType(DateTime.UtcNow);

        var newSchemaVersion = PosNum<int>.Create(effectType.SchemaVersion).Value;

        // Act
        effectType.UpdateSchemaVersion(newSchemaVersion, DateTime.UtcNow);

        // Assert
        effectType.GetDomainEvents().ShouldBeEmpty();
    }
}
