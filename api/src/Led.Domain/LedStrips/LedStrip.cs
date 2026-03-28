using FluentResults;
using Led.Domain.LedStrips.Events;
using Led.Domain.LedStrips.ValueObjects;
using Led.Domain.Shared.ValueObjects;
using Led.SharedKernal.DDD;

namespace Led.Domain.LedStrips;

public sealed class LedStrip : AggregateRoot<Guid>
{
    public Guid TenantId { get; private set; }
    public Guid DeviceId { get; private set; }
    public LedStripType StripTypeId { get; private set; }
    public LedStripName Name { get; private set; }
    public PosNum<short> GpioPin { get; private set; }
    public PosNum<short> LedCount { get; private set; }
    public PosNum<int> Frequency { get; private set; }
    public PosNum<short> DmaChannel { get; private set; }
    public PosNum<short> Brightness { get; private set; }
    public bool Invert { get; private set; }
    public PosNum<short> Voltage { get; private set; }
    public PosNum<int> MaxCurrentMa { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }

    private LedStrip()
    { }

    private LedStrip(Guid id,
                     Guid userId,
                     Guid deviceId,
                     LedStripType stripTypeId,
                     LedStripName name,
                     PosNum<short> gpioPin,
                     PosNum<short> ledCount,
                     PosNum<int> frequency,
                     PosNum<short> dmaChannel,
                     PosNum<short> brightness,
                     bool invert,
                     PosNum<short> voltage,
                     PosNum<int> maxCurrentMa,
                     DateTime createdAt)
    : base(id)
    {
        TenantId = userId;
        DeviceId = deviceId;
        StripTypeId = stripTypeId;
        Name = name;
        GpioPin = gpioPin;
        LedCount = ledCount;
        Frequency = frequency;
        DmaChannel = dmaChannel;
        Brightness = brightness;
        Invert = invert;
        Voltage = voltage;
        MaxCurrentMa = maxCurrentMa;
        CreatedAt = createdAt;
    }

    public static Result<LedStrip> Create(Guid tenantId,
                                          Guid deviceId,
                                          LedStripType stripTypeId,
                                          LedStripName name,
                                          PosNum<short> gpioPin,
                                          PosNum<short> ledCount,
                                          PosNum<int> frequency,
                                          PosNum<short> dmaChannel,
                                          PosNum<short> brightness,
                                          bool invert,
                                          PosNum<short> voltage,
                                          PosNum<int> maxCurrentMa,
                                          DateTime createdAt)
    {

        var ledStrip = new LedStrip(Guid.CreateVersion7(),
                                    tenantId,
                                    deviceId,
                                    stripTypeId,
                                    name,
                                    gpioPin,
                                    ledCount,
                                    frequency,
                                    dmaChannel,
                                    brightness,
                                    invert,
                                    voltage,
                                    maxCurrentMa,
                                    createdAt);

        return ledStrip;
    }

    public void Update(LedStripType stripTypeId,
                       LedStripName name,
                       PosNum<short> gpioPin,
                       PosNum<short> ledCount,
                       PosNum<int> frequency,
                       PosNum<short> dmaChannel,
                       PosNum<short> brightness,
                       bool invert,
                       PosNum<short> voltage,
                       PosNum<int> maxCurrentMa,
                       DateTime modifiedAt)
    {
        Name = name;

        StripTypeId = stripTypeId;

        GpioPin = gpioPin;
        LedCount = ledCount;
        Frequency = frequency;
        DmaChannel = dmaChannel;
        Invert = invert;
        Voltage = voltage;
        MaxCurrentMa = maxCurrentMa;

        Brightness = brightness;

        ModifiedAt = modifiedAt;
    }

    public void UpdateBrightness(PosNum<short> brightness, DateTime modifiedAt)
    {
        Brightness = brightness;
        ModifiedAt = modifiedAt;

        RaiseDomainEvent(new DeviceBrightnesssUpdatedDomainEvent(Id, Brightness.Value));
    }
}
