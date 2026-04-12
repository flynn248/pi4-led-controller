using FluentValidation;

namespace Led.Application.LedStrips.AddLedStrip;

internal sealed class AddLedStripCommandValidator : AbstractValidator<AddLedStripCommand>
{
    public AddLedStripCommandValidator()
    {
        RuleFor(r => r.TenantId).NotEmpty();
        RuleFor(r => r.DeviceId).NotEmpty();
        RuleFor(r => r.LedStripTypeId).NotEmpty().IsInEnum();
        RuleFor(r => r.Name).NotEmpty();
        RuleFor(r => r.GpioPin).NotEmpty();
        RuleFor(r => r.LedCount).NotEmpty();
        RuleFor(r => r.Brightness).NotNull();
        RuleFor(r => r.Invert).NotNull();
        RuleFor(r => r.Voltage).NotEmpty();
        RuleFor(r => r.MaxCurrentMa).NotEmpty();
    }
}
