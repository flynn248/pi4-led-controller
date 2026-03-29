using Led.Domain.Scenes.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.Scenes;

public sealed class Scene : AggregateRoot<Guid>
{
    public Guid TenantId { get; private set; }
    public SceneName Name { get; private set; }
    public SceneDescription Description { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? ModifiedAtUtc { get; private set; }

    private Scene()
    { }

    private Scene(Guid id, Guid tenantId, SceneName name, SceneDescription description, DateTime createdAtUtc)
        : base(id)
    {
        TenantId = tenantId;
        Name = name;
        Description = description;
        CreatedAtUtc = createdAtUtc;
    }

    public static Scene Create(Guid tenantId, SceneName name, SceneDescription description, DateTime createdAtUtc)
    {
        return new Scene(Guid.CreateVersion7(), tenantId, name, description, createdAtUtc);
    }

    public void Update(SceneName name, SceneDescription description, DateTime modifiedAtUtc)
    {
        Name = name;
        Description = description;
        ModifiedAtUtc = modifiedAtUtc;
    }
}
