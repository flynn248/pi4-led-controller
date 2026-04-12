using FluentResults;
using Led.Domain.LedStrips;
using Led.Domain.LedStrips.Repositories;
using Led.Domain.LedStrips.ValueObjects;
using Led.Domain.Shared.ValueObjects;
using Led.SharedKernal.Clock;
using Led.SharedKernal.UoW;
using LiteBus.Commands.Abstractions;

namespace Led.Application.LedStrips.AddLedStrip;

internal sealed class AddLedStripCommandHandler(IUnitOfWorkManager unitOfWorkManager,
                                                IDateTimeProvider dateTimeProvider,
                                                ILedStripRepository ledStripRepository) : ICommandHandler<AddLedStripCommand, Result<Guid>>
{
    public async Task<Result<Guid>> HandleAsync(AddLedStripCommand message, CancellationToken cancellationToken = default)
    {
        var name = LedStripName.Create(message.Name);
        var gpioPin = PosNum<short>.Create(message.GpioPin);
        var ledCount = PosNum<short>.Create(message.LedCount);
        var frequency = PosNum<int>.Create(message.Frequency);
        var dmaChannel = PosNum<short>.Create(message.DmaChannel);
        var brightness = ZeroOrPosNum<short>.Create(message.Brightness);
        var voltage = PosNum<short>.Create(message.Voltage);
        var maxCurrentMa = PosNum<int>.Create(message.MaxCurrentMa);

        var overall = Result.Merge(name, gpioPin, ledCount, frequency, dmaChannel, brightness, voltage, maxCurrentMa);

        if (overall.IsFailed)
        {
            return Result.Fail(overall.Errors);
        }

        var ledStrip = LedStrip.Create(message.TenantId, message.DeviceId, message.LedStripTypeId, name.Value, gpioPin.Value, ledCount.Value, frequency.Value, dmaChannel.Value, brightness.Value, message.Invert, voltage.Value, maxCurrentMa.Value, dateTimeProvider.UtcNow);

        using var uow = unitOfWorkManager.Begin();

        ledStripRepository.Add(ledStrip);

        await uow.SaveChanges(cancellationToken);

        return ledStrip.Id;
    }
}
