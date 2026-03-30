using Led.Domain.Scenes.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.Scenes;

public sealed class LedState : IEntity
{
    public Guid EffectId { get; init; }
    public short LedIndex { get; private set; }
    public short Red { get; private set; }
    public short Green { get; private set; }
    public short Blue { get; private set; }
    public short White { get; private set; }

    private LedState()
    { }

    private LedState(Guid effectId, LedIndex ledIndex, LedColor red, LedColor green, LedColor blue, LedColor white)
    {
        EffectId = effectId;
        LedIndex = ledIndex.Value;
        Red = red.Value;
        Green = green.Value;
        Blue = blue.Value;
        White = white.Value;
    }

    public static LedState Create(Guid effectId, LedIndex ledIndex, LedColor red, LedColor green, LedColor blue, LedColor white)
    {
        return new LedState(effectId, ledIndex, red, green, blue, white);
    }

    public void Update(LedIndex ledIndex, LedColor red, LedColor green, LedColor blue, LedColor white)
    {
        LedIndex = ledIndex.Value;
        Red = red.Value;
        Green = green.Value;
        Blue = blue.Value;
        White = white.Value;
    }
}
