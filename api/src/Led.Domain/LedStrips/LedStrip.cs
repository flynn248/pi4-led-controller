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
    public LedStripTypeId LedStripTypeId { get; private set; }
    public LedStripName Name { get; private set; }
    public PosNum<short> GpioPin { get; private set; }
    public PosNum<short> LedCount { get; private set; }
    public PosNum<int> Frequency { get; private set; }
    public PosNum<short> DmaChannel { get; private set; }
    public PosNum<short> Brightness { get; private set; }
    public bool Invert { get; private set; }
    public PosNum<short> Voltage { get; private set; }
    public PosNum<int> MaxCurrentMa { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? ModifiedAtUtc { get; private set; }

    private LedStrip()
    { }

    private LedStrip(Guid id,
                     Guid userId,
                     Guid deviceId,
                     LedStripTypeId ledStripTypeId,
                     LedStripName name,
                     PosNum<short> gpioPin,
                     PosNum<short> ledCount,
                     PosNum<int> frequency,
                     PosNum<short> dmaChannel,
                     PosNum<short> brightness,
                     bool invert,
                     PosNum<short> voltage,
                     PosNum<int> maxCurrentMa,
                     DateTime createdAtUtc)
    : base(id)
    {
        TenantId = userId;
        DeviceId = deviceId;
        LedStripTypeId = ledStripTypeId;
        Name = name;
        GpioPin = gpioPin;
        LedCount = ledCount;
        Frequency = frequency;
        DmaChannel = dmaChannel;
        Brightness = brightness;
        Invert = invert;
        Voltage = voltage;
        MaxCurrentMa = maxCurrentMa;
        CreatedAtUtc = createdAtUtc;
    }

    public static Result<LedStrip> Create(Guid tenantId,
                                          Guid deviceId,
                                          LedStripTypeId ledStripTypeId,
                                          LedStripName name,
                                          PosNum<short> gpioPin,
                                          PosNum<short> ledCount,
                                          PosNum<int> frequency,
                                          PosNum<short> dmaChannel,
                                          PosNum<short> brightness,
                                          bool invert,
                                          PosNum<short> voltage,
                                          PosNum<int> maxCurrentMa,
                                          DateTime createdAtUtc)
    {

        var ledStrip = new LedStrip(Guid.CreateVersion7(),
                                    tenantId,
                                    deviceId,
                                    ledStripTypeId,
                                    name,
                                    gpioPin,
                                    ledCount,
                                    frequency,
                                    dmaChannel,
                                    brightness,
                                    invert,
                                    voltage,
                                    maxCurrentMa,
                                    createdAtUtc);

        return ledStrip;
    }

    public void Update(LedStripTypeId ledStripTypeId,
                       LedStripName name,
                       PosNum<short> gpioPin,
                       PosNum<short> ledCount,
                       PosNum<int> frequency,
                       PosNum<short> dmaChannel,
                       PosNum<short> brightness,
                       bool invert,
                       PosNum<short> voltage,
                       PosNum<int> maxCurrentMa,
                       DateTime modifiedAtUtc)
    {
        Name = name;

        LedStripTypeId = ledStripTypeId;

        GpioPin = gpioPin;
        LedCount = ledCount;
        Frequency = frequency;
        DmaChannel = dmaChannel;
        Invert = invert;
        Voltage = voltage;
        MaxCurrentMa = maxCurrentMa;

        Brightness = brightness;

        ModifiedAtUtc = modifiedAtUtc;
    }

    public void UpdateBrightness(PosNum<short> brightness, DateTime modifiedAtUtc)
    {
        Brightness = brightness;
        ModifiedAtUtc = modifiedAtUtc;

        RaiseDomainEvent(new DeviceBrightnesssUpdatedDomainEvent(Id, Brightness.Value));
    }
}
