using FluentValidation;

namespace Led.Application.Devices.VerifyConnection;

internal sealed class VerifyDeviceConnectionCommandValidator : AbstractValidator<VerifyDeviceConnectionCommand>
{
    public VerifyDeviceConnectionCommandValidator()
    {
        RuleFor(e => e.IpAddress).NotEmpty();
        RuleFor(e => e.Username).NotEmpty();
        RuleFor(e => e.Password).NotEmpty();
    }
}
