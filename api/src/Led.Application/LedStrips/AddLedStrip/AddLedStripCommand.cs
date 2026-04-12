using FluentResults;
using Led.Domain.LedStrips.ValueObjects;
using LiteBus.Commands.Abstractions;

namespace Led.Application.LedStrips.AddLedStrip;

public sealed record AddLedStripCommand(Guid TenantId,
                                        Guid DeviceId,
                                        LedStripTypeId LedStripTypeId,
                                        string Name,
                                        short GpioPin,
                                        short LedCount,
                                        int Frequency,
                                        short DmaChannel,
                                        short Brightness,
                                        bool Invert,
                                        short Voltage,
                                        int MaxCurrentMa) : ICommand<Result<Guid>>;
