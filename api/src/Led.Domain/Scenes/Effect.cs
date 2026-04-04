using Led.Domain.Scenes.Events;
using Led.Domain.Scenes.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.Scenes;

public sealed class Effect : AggregateRoot<Guid>
{
    public Guid SceneId { get; private set; }
    public Guid LedStripId { get; private set; }
    public Guid EffectTypeId { get; private set; }
    public EffectParameter ParameterJson { get; private set; }
    public int ParameterJsonSchemaVersion { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? ModifiedAtUtc { get; private set; }

    private Effect()
    { }

    private Effect(Guid id, Guid sceneId, Guid ledStripId, Guid effectTypeId, EffectParameter parameterJson, int parameterJsonSchemaVersion, DateTime createdAtUtc)
        : base(id)
    {
        SceneId = sceneId;
        LedStripId = ledStripId;
        EffectTypeId = effectTypeId;
        ParameterJson = parameterJson;
        ParameterJsonSchemaVersion = parameterJsonSchemaVersion;
        CreatedAtUtc = createdAtUtc;
    }

    public static Effect Create(Guid sceneId, Guid ledStripId, Guid effectTypeId, EffectParameter parameterJson, int parameterJsonSchemaVersion, DateTime createdAtUtc)
    {
        return new Effect(Guid.CreateVersion7(), sceneId, ledStripId, effectTypeId, parameterJson, parameterJsonSchemaVersion, createdAtUtc);
    }

    public void UpdateParameters(EffectParameter parameterJson, int parameterJsonSchemaVersion, DateTime modifiedAtUtc)
    {
        if (ParameterJson == parameterJson
            && ParameterJsonSchemaVersion == parameterJsonSchemaVersion)
        {
            return;
        }

        ParameterJson = parameterJson;
        ParameterJsonSchemaVersion = parameterJsonSchemaVersion;
        ModifiedAtUtc = modifiedAtUtc;

        RaiseDomainEvent(new SceneUpdatedDomainEvent(SceneId, LedStripId));
    }
}
