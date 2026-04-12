using FluentValidation;

namespace Led.Application.Users.UpdateEmail;

internal sealed class UpdateEmailCommandValidator : AbstractValidator<UpdateEmailCommand>
{
    public UpdateEmailCommandValidator()
    {
        RuleFor(r => r.UserId).NotEmpty();
        RuleFor(r => r.NewEmail).NotEmpty().EmailAddress();
    }
}
