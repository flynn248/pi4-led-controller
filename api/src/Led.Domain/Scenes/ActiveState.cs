using Led.Domain.Scenes.Events;
using Led.Domain.Scenes.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.Scenes;

public sealed class ActiveState : AggregateRoot
{
    public Guid LedStripId { get; init; }
    public Guid TenantId { get; init; }
    public Guid DeviceId { get; init; }
    public Guid SceneId { get; private set; }
    public ActiveStateSource Source { get; private set; }
    public DateTime ActivatedAtUtc { get; private set; }

    private ActiveState()
    { }

    private ActiveState(Guid tenantId, Guid ledStripId, Guid deviceId, Guid sceneId, ActiveStateSource source, DateTime activatedAtUtc)
    {
        TenantId = tenantId;
        LedStripId = ledStripId;
        DeviceId = deviceId;
        SceneId = sceneId;
        Source = source;
        ActivatedAtUtc = activatedAtUtc;
    }

    public static ActiveState Create(Guid tenantId, Guid ledStripId, Guid deviceId, Guid sceneId, ActiveStateSource source, DateTime activatedAtUtc)
    {
        var state = new ActiveState(tenantId, ledStripId, deviceId, sceneId, source, activatedAtUtc);

        state.RaiseDomainEvent(new ActiveStateCreatedOrUpdatedDomainEvent(state.LedStripId, state.DeviceId, state.SceneId));

        return state;
    }

    public void UpdateScene(Guid sceneId, ActiveStateSource source, DateTime activatedAtUtc)
    {
        if (SceneId == sceneId)
        {
            return;
        }

        SceneId = sceneId;
        Source = source;
        ActivatedAtUtc = activatedAtUtc;

        RaiseDomainEvent(new ActiveStateCreatedOrUpdatedDomainEvent(LedStripId, DeviceId, SceneId));
    }
}
