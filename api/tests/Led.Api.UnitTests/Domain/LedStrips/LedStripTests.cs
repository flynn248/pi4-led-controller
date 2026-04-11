using Led.Domain.LedStrips;
using Led.Domain.LedStrips.Events;
using Led.Domain.LedStrips.ValueObjects;
using Led.Domain.Shared.ValueObjects;
using Led.SharedKernal.DDD;
using Shouldly;

namespace Led.Api.UnitTests.Domain.LedStrips;

public class LedStripTests
{
    [Theory]
    [MemberData(nameof(Update_Should_Raise_LedStripUpdatedDomainEvent_TestData))]
    public void Update_Should_Raise_LedStripUpdatedDomainEvent(LedStrip newValue)
    {
        // Arrange
        var existingStrip = GetDefault(newValue.CreatedAtUtc);

        var expectedDomainEvents = new List<IDomainEvent>() { new LedStripUpdatedDomainEvent(existingStrip.Id) }.AsReadOnly();

        // Act
        existingStrip.Update(newValue.LedStripTypeId, newValue.Name, newValue.GpioPin, newValue.LedCount, newValue.Frequency, newValue.DmaChannel, newValue.Brightness, newValue.Invert, newValue.Voltage, newValue.MaxCurrentMa, DateTime.UtcNow);

        // Assert
        existingStrip.GetDomainEvents().ShouldBeEquivalentTo(expectedDomainEvents);
    }

    public static IEnumerable<TheoryDataRow<LedStrip>> Update_Should_Raise_LedStripUpdatedDomainEvent_TestData()
    {
        var defaultStrip = GetDefault(DateTime.Now);

        yield return LedStrip.Create(defaultStrip.TenantId,
                                     defaultStrip.DeviceId,
                                     defaultStrip.LedStripTypeId,
                                     defaultStrip.Name,
                                     PosNum<short>.Create((short)7).Value,
                                     defaultStrip.LedCount,
                                     defaultStrip.Frequency,
                                     defaultStrip.DmaChannel,
                                     defaultStrip.Brightness,
                                     defaultStrip.Invert,
                                     defaultStrip.Voltage,
                                     defaultStrip.MaxCurrentMa,
                                     defaultStrip.CreatedAtUtc).Value;

        yield return LedStrip.Create(defaultStrip.TenantId,
                                     defaultStrip.DeviceId,
                                     defaultStrip.LedStripTypeId,
                                     defaultStrip.Name,
                                     defaultStrip.GpioPin,
                                     PosNum<short>.Create((short)7).Value,
                                     defaultStrip.Frequency,
                                     defaultStrip.DmaChannel,
                                     defaultStrip.Brightness,
                                     defaultStrip.Invert,
                                     defaultStrip.Voltage,
                                     defaultStrip.MaxCurrentMa,
                                     defaultStrip.CreatedAtUtc).Value;

        yield return LedStrip.Create(defaultStrip.TenantId,
                                     defaultStrip.DeviceId,
                                     defaultStrip.LedStripTypeId,
                                     defaultStrip.Name,
                                     defaultStrip.GpioPin,
                                     defaultStrip.LedCount,
                                     PosNum<short>.Create(7).Value,
                                     defaultStrip.DmaChannel,
                                     defaultStrip.Brightness,
                                     defaultStrip.Invert,
                                     defaultStrip.Voltage,
                                     defaultStrip.MaxCurrentMa,
                                     defaultStrip.CreatedAtUtc).Value;

        yield return LedStrip.Create(defaultStrip.TenantId,
                                     defaultStrip.DeviceId,
                                     defaultStrip.LedStripTypeId,
                                     defaultStrip.Name,
                                     defaultStrip.GpioPin,
                                     defaultStrip.LedCount,
                                     defaultStrip.Frequency,
                                     PosNum<short>.Create((short)7).Value,
                                     defaultStrip.Brightness,
                                     defaultStrip.Invert,
                                     defaultStrip.Voltage,
                                     defaultStrip.MaxCurrentMa,
                                     defaultStrip.CreatedAtUtc).Value;

        yield return LedStrip.Create(defaultStrip.TenantId,
                                     defaultStrip.DeviceId,
                                     defaultStrip.LedStripTypeId,
                                     defaultStrip.Name,
                                     defaultStrip.GpioPin,
                                     defaultStrip.LedCount,
                                     defaultStrip.Frequency,
                                     defaultStrip.DmaChannel,
                                     PosNum<short>.Create((short)7).Value,
                                     defaultStrip.Invert,
                                     defaultStrip.Voltage,
                                     defaultStrip.MaxCurrentMa,
                                     defaultStrip.CreatedAtUtc).Value;

        yield return LedStrip.Create(defaultStrip.TenantId,
                                     defaultStrip.DeviceId,
                                     defaultStrip.LedStripTypeId,
                                     defaultStrip.Name,
                                     defaultStrip.GpioPin,
                                     defaultStrip.LedCount,
                                     defaultStrip.Frequency,
                                     defaultStrip.DmaChannel,
                                     defaultStrip.Brightness,
                                     !defaultStrip.Invert,
                                     defaultStrip.Voltage,
                                     defaultStrip.MaxCurrentMa,
                                     defaultStrip.CreatedAtUtc).Value;

        yield return LedStrip.Create(defaultStrip.TenantId,
                                     defaultStrip.DeviceId,
                                     defaultStrip.LedStripTypeId,
                                     defaultStrip.Name,
                                     defaultStrip.GpioPin,
                                     defaultStrip.LedCount,
                                     defaultStrip.Frequency,
                                     defaultStrip.DmaChannel,
                                     defaultStrip.Brightness,
                                     defaultStrip.Invert,
                                     PosNum<short>.Create((short)7).Value,
                                     defaultStrip.MaxCurrentMa,
                                     defaultStrip.CreatedAtUtc).Value;


        yield return LedStrip.Create(defaultStrip.TenantId,
                                     defaultStrip.DeviceId,
                                     defaultStrip.LedStripTypeId,
                                     defaultStrip.Name,
                                     defaultStrip.GpioPin,
                                     defaultStrip.LedCount,
                                     defaultStrip.Frequency,
                                     defaultStrip.DmaChannel,
                                     defaultStrip.Brightness,
                                     defaultStrip.Invert,
                                     defaultStrip.Voltage,
                                     PosNum<short>.Create(7).Value,
                                     defaultStrip.CreatedAtUtc).Value;
    }

    private static LedStrip GetDefault(DateTime createdAtUtc) => LedStrip.Create(tenantId: Guid.CreateVersion7(),
                                                                                 deviceId: Guid.CreateVersion7(),
                                                                                 ledStripTypeId: LedStripTypeId.SK6812_RGBW,
                                                                                 name: LedStripName.Create("default").Value,
                                                                                 gpioPin: PosNum<short>.Create((short)1).Value,
                                                                                 ledCount: PosNum<short>.Create((short)1).Value,
                                                                                 frequency: PosNum<int>.Create(1).Value,
                                                                                 dmaChannel: PosNum<short>.Create((short)1).Value,
                                                                                 brightness: PosNum<short>.Create((short)1).Value,
                                                                                 invert: false,
                                                                                 voltage: PosNum<short>.Create((short)1).Value,
                                                                                 maxCurrentMa: PosNum<int>.Create(1).Value,
                                                                                 createdAtUtc).Value;
}
